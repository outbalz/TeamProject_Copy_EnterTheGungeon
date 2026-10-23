using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System;

public class WeaponController : MonoBehaviour
{
    #region 인스펙터
    [Header("무기 설정")]
    [SerializeField] private CWeaponDatabaseSO _database = null;
    [SerializeField] private string _weaponId = "Weapon_Pistol";
    [SerializeField] private Transform _firePoint = null;
    [SerializeField] private Transform _weaponTransform;

    [Header("스프라이트 설정")]
    [SerializeField] private SpriteRenderer _weaponSpriteRenderer;
    [SerializeField] private SpriteRenderer _fireSpriteRenderer;
    [SerializeField] private float _fireSpriteTime = 0.1f;
    [SerializeField] private float _rotationSharpness = 25f;
    [SerializeField] Transform _gunTranform;
    #endregion

    #region 내부 변수
    private CWeaponDataSO _currentWeapon;
    private CancellationTokenSource _fireCTS; // 발사 취소
    private CancellationTokenSource _reloadCTS; // 장전 취소
    private CancellationToken _fireSpriteAnimCTS;
    private GameObject _muzzleFlash;

    private int _currentAmmo;
    private int _currentTotalAmmo;
    private bool _isReloading = false;
    private float _lastFireTime = 0f;
    #endregion

    #region 프로퍼티
    public CWeaponDataSO CurrentWeapon => _currentWeapon;
    public int CurrentAmmo => _currentAmmo;
    public bool IsReloading => _isReloading;
    #endregion

    private Vector3 _originPos;

    private void Start()
    {
        if (_weaponTransform != null)
        {
            _originPos = _weaponTransform.localPosition;
        }

        SelectWeapon(_weaponId);
    }

    // 발사, 장전 구독
    private void OnEnable()
    {
        if (CInputDispatcher.Instance != null)
        {
            if (CInputDispatcher.Instance.InputActionMap.TryGetValue("Fire", out var fireAction))
            {
                fireAction.OnPerformedAction += TryFire;
            }
            if (CInputDispatcher.Instance.InputActionMap.TryGetValue("Reload", out var reloadAction))
            {
                reloadAction.OnPerformedAction += TryReload;
            }
        }
    }

    // 발사, 장전 구독해제
    private void OnDisable()
    {
        if (CInputDispatcher.Instance != null)
        {
            if (CInputDispatcher.Instance.InputActionMap.TryGetValue("Fire", out var fireAction))
            {
                fireAction.OnPerformedAction -= TryFire;
            }
            if (CInputDispatcher.Instance.InputActionMap.TryGetValue("Reload", out var reloadAction))
            {
                reloadAction.OnPerformedAction -= TryReload;
            }
        }
    }
    private void Update()
    {
        Fire();
        RecoveryRevound();
    }


    private void OnDestroy()
    {
        CancelFire();
        CancelReload();
    }

    // 무기선택
    public bool SelectWeapon(string id)
    {
        if (_database == null)
        {
            return false;
        }

        if (!_database.TryGet(id, out CWeaponDataSO weapon))
        {
            Debug.Log($"무기를 찾을 수 없다.{id}");

            return false;
        }

        Debug.Log($" 무기id {id}");

   
     

        _currentWeapon = weapon;
        _weaponId = id;

        // 탄환
        _currentAmmo = _currentWeapon.MaxAmmo;
        _currentTotalAmmo = _currentWeapon.MaxTotalAmmo;

        // 무기 이미지 장착 
        if (_weaponSpriteRenderer != null && _currentWeapon.WeaponSprite != null)
        {
            _weaponSpriteRenderer.sprite = _currentWeapon.WeaponSprite; 
        }

        Debug.Log($"무기 장착 성공: {_currentWeapon.name}");
        return true;
    }

    // 발사시도
    public void TryFire()
    {
        Debug.Log("발사");
        if (_currentWeapon == null || _firePoint == null)
        {
            return;
        }

        if (_currentWeapon.Trigger == null || _currentWeapon.Projectile == null)
        {
            return;
        }

        if (_isReloading)
        {
            return;
        }

        // 현재 탄약이 0보다 작으면 장전
        if (_currentAmmo <= 0)
        {
            TryReload();
            return;
        }


        CancelFire();
        _fireCTS = new CancellationTokenSource();

        CWeaponDataSO weapon = _currentWeapon;

        CWeaponTriggerContext context = new CWeaponTriggerContext(
            weapon,
            _firePoint,
            SpawnProjectile
            );

        weapon.Trigger.TriggerWeapon(context, _fireCTS.Token).Forget();

    }

    private void Fire()
    {
        // 마우스 위치 계산
        Vector2 mousPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint( mousPos );
        mouseWorldPos.z = 0f;

        // 마우스 방향 계산
        Vector3 direction = (mouseWorldPos - _gunTranform.position).normalized;
        Quaternion targetRotatiom = Quaternion.FromToRotation(Vector3.up, direction);

        // 보간
        float t = 1.0f - Mathf.Exp(-_rotationSharpness * Time.deltaTime);
        _gunTranform.rotation = Quaternion.Slerp(_gunTranform.rotation, targetRotatiom, t);

        if (_gunTranform != null)
        {
            Vector3 localScale = _gunTranform.localScale;

            // 마우스 좌표, 총 좌표 비교
            if (mouseWorldPos.x < _gunTranform.position.x)
            {
                localScale.x = 1f; 
            }
            else
            {
                localScale.x = -1f;  
            }
            _gunTranform.localScale = localScale;
        }


    }

  
    // 장전 시도
    private void TryReload()
    {
        Debug.Log($"현재 탄약 {_currentAmmo} | 예비 탄약{_currentTotalAmmo}");
        // 유효성 검사
        if (_currentWeapon == null || _currentWeapon.Reload == null) 
        {
            return;
        }

        // 장전중이면 X
        if (_isReloading)
        {
            return;
        }

        // 최대 탄약 넘으면 x
        if (_currentAmmo >= _currentWeapon.MaxAmmo)
        {
            Debug.Log("탄창이 가득참");
            return;
        }

        // 탄약 없을경우 장전 불가
        if (_currentTotalAmmo <= 0)
        {
            return;
        }

        Debug.Log("장전 시작");
        CancelFire();
        CancelReload();

        _reloadCTS = new CancellationTokenSource();
        _isReloading = true;

        // 데이터 값 들고오기
        CWeaponDataSO weapon = _currentWeapon;

        CWeaponReloadContext reloadContext = new CWeaponReloadContext(
            weapon,
              _currentAmmo,
            LoadComplete
            );

        ReloadAsync(reloadContext, _reloadCTS.Token).Forget();

    }

    // 반동
    private void RecoveryRevound()
    {
        if (_weaponTransform != null)
        {

            _weaponTransform.localPosition = Vector3.Lerp(
                _weaponTransform.localPosition,
                _originPos, 
                Time.deltaTime * _currentWeapon.ReboundSpeed);
        }
    }

    private void AppltReload()
    {
        if (_weaponTransform == null)
        {
            return;
        }

        _weaponTransform.localPosition = _originPos + (Vector3.down * _currentWeapon.ReboundRange);

        Debug.Log("반동o");
    }

    private async UniTask ReloadAsync(CWeaponReloadContext context, CancellationToken ct)
    {
        bool success = await _currentWeapon.Reload.ReloadWeapon(context, ct);

        if (success)
        {
            Debug.Log("장전 O");
        }

        else
        {
            Debug.Log("장전 x");
        }
        _isReloading = false;
    }

    
    private async UniTask ReturnFireSpriteAnim()
    {
        if (_weaponSpriteRenderer == null || _fireSpriteRenderer == null)
        {
            return;
        }

        
        _weaponSpriteRenderer.sprite = _fireSpriteRenderer.sprite;

        float currentFireTime = Time.deltaTime;
        _lastFireTime = currentFireTime;

        await UniTask.Delay(TimeSpan.FromSeconds(_fireSpriteTime), DelayType.UnscaledDeltaTime);

        if (_lastFireTime == currentFireTime)
        {
            _weaponSpriteRenderer.sprite = _currentWeapon.WeaponSprite;
        }


    }

    // 발사 머블 플레쉬
    private async UniTask FireFlashMuzzle()
    {
        if (_muzzleFlash == null)
        {
            return;
        }

         _muzzleFlash.SetActive(true);

        await UniTask.Delay(TimeSpan.FromSeconds(_currentWeapon.MuzzleFlashTime), DelayType.UnscaledDeltaTime);

        if (_muzzleFlash != null)
        {
            _muzzleFlash.SetActive(false);
        }
    }

    private void SpawnProjectile(Quaternion rotation)
    {
        if (_currentWeapon == null)
        {
            return ;
        }

        if (_currentAmmo <= 0)
        {
            CancelFire();
            return;
        }

        SpawnProjectile(_currentWeapon, rotation);
    }

    private void LoadComplete(int ammo)
    {
        if (_currentWeapon == null)
        {
            return;
        }

        int neededAmmo = _currentWeapon.MaxAmmo - _currentAmmo;
        int ammoToReload = Mathf.Min(neededAmmo, _currentTotalAmmo);

        _currentAmmo += ammoToReload;
        _currentTotalAmmo -= ammoToReload;
    }



    // 투사체 꺼내오기
    private void SpawnProjectile(CWeaponDataSO weapon, Quaternion rotation)
    {
        if (weapon == null || weapon.Projectile == null)
        {
            return;
        }

        // 생성
        ProjectileController projectile = CProjectileFactory.Instance.Creat();

        Debug.Log($"{_firePoint.position}");

        if (projectile == null )
        {
            return;
        }

        projectile.transform.SetParent(null);

        if (_firePoint != null)
        {
            projectile.transform.position = _firePoint.position;
            projectile.transform.rotation = rotation;
         
        }

        else
        {
            projectile.transform.position = transform.position;
            projectile.transform.rotation = rotation;

        }
        projectile.gameObject.SetActive(true);
        projectile.SetUp(weapon.Projectile);

        // 총알 감소
        _currentAmmo--;
        Debug.Log($"{projectile.transform.position}");

        AppltReload();

        ReturnFireSpriteAnim().Forget();
        FireFlashMuzzle().Forget();
    }



    // 취소됬을경우
    private void CancelFire()
    {
        if (_fireCTS == null)
        {
            return;
        }

        _fireCTS.Cancel();
        _fireCTS.Dispose();

        _fireCTS = null;

    }

    private void CancelReload()
    {
        if (_reloadCTS == null)
        {
            return;
        }

        _reloadCTS.Cancel();
        _reloadCTS.Dispose();
        _reloadCTS = null;
    }




}
