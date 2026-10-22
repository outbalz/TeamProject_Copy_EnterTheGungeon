using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    #region 인스펙터
    [Header("무기 설정")]
    [SerializeField] private CWeaponDatabaseSO _database = null;
    [SerializeField] private string _weaponId = "Weapon_Pistol";
    [SerializeField] private Transform _firePoint = null;
    [SerializeField] private Transform _weaponTransform;

    [SerializeField] private SpriteRenderer _weaponSpriteRenderer;
    [SerializeField] private SpriteRenderer _fireSpriteRenderer;

    [Header("반동 설정")]
    [SerializeField] private float _reboundRange = 0.2f;
    [SerializeField] private float _reboundSpeed = 10f;
    #endregion

    #region 내부 변수
    private CWeaponDataSO _currentWeapon;
    private CancellationTokenSource _fireCTS; // 발사 취소
    private CancellationTokenSource _reloadCTS; // 장전 취소

    private int _currentAmmo;
    private int _currentTotalAmmo;
    private bool _isReloading = false;
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
    private void Update()
    {
        //유효성 검사
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                TryFire();
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                CancelFire();
            }
        }

        if (Keyboard.current != null)
        {
            if (Keyboard.current.rKey.wasReleasedThisFrame)
            {
                TryReload();
            }

        }

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
            HandleSpawnProjectile
            );

        weapon.Trigger.TriggerWeapon(context, _fireCTS.Token).Forget();

        // 발사 이미지 장착 
        if (_fireSpriteRenderer != null && _fireSpriteRenderer.sprite != null)
        {
            _weaponSpriteRenderer.sprite = _fireSpriteRenderer.sprite;
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
            HandleReloadComplete
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
                Time.deltaTime * _reboundSpeed);
        }
    }

    private void AppltReload()
    {
        if (_weaponTransform == null)
        {
            return;
        }

        _weaponTransform.localPosition = _originPos + (Vector3.down * _reboundRange);

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

    private void HandleSpawnProjectile(Quaternion rotation)
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

    private void HandleReloadComplete(int ammo)
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
