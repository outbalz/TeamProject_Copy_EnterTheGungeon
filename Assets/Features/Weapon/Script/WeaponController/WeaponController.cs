using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour
{
    #region �ν�����
    [Header("���� ����")]
    [SerializeField] private CWeaponDatabaseSO _database = null;
    [SerializeField] private string _weaponId = "Weapon_Pistol";
    [SerializeField] private Transform _firePoint = null;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private GameObject _muzzleFlash;
    // ���� ���
    [SerializeField] private List<string> _weaponNames = new List<string>();

    [Header("��������Ʈ ����")]
    [SerializeField] private SpriteRenderer _weaponSpriteRenderer;
    [SerializeField] private float _fireSpriteTime = 0.1f;
    [SerializeField] private float _rotationSharpness = 25f;
   
    #endregion

    #region ���� ����
    private CWeaponDataSO _currentWeapon;
    private string _currentWeaponId;
    private int _currentWeaponIndex = 0;
    private CancellationTokenSource _fireCTS; // �߻� ���
    private CancellationTokenSource _reloadCTS; // ���� ���
    private CancellationToken _fireSpriteAnimCTS;
    private Dictionary<string, int> _ammo = new Dictionary<string, int>();
    private Dictionary<string, int> _totalAmmo = new Dictionary<string, int>();

    private int _currentAmmo;
    private int _currentTotalAmmo;
    private bool _isReloading = false;
    private float _lastFireTime = 0f;
    #endregion

    #region ������Ƽ
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

        _muzzleFlash.SetActive(false);
    }

    // �߻�, ����, ����ġ ����
    private void OnEnable()
    {
        if (CInputDispatcher.Instance != null)
        {
            if (CInputDispatcher.Instance.InputActionMap.TryGetValue("TriggerWeapon", out var fireAction))
            {
                fireAction.OnPerformedAction += TryFire;
            }
            if (CInputDispatcher.Instance.InputActionMap.TryGetValue("Reload", out var reloadAction))
            {
                reloadAction.OnPerformedAction += TryReload;
            }

            if (CInputDispatcher.Instance.InputActionMap.TryGetValue("Scroll", out var swichAction))
            {
                swichAction.OnPerformedAction += TrySwitchWeapon;
            }
        }
    }

    // �߻�, ����, ����ġ ��������
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

            if (CInputDispatcher.Instance.InputActionMap.TryGetValue("Scroll", out var swichAction))
            {
                swichAction.OnPerformedAction -= TrySwitchWeapon;
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
    
    // ���⼱��
    public bool SelectWeapon(string id)
    {
        if (_database == null)
        {
            return false;
        }

        if (!_database.TryGet(id, out CWeaponDataSO weapon))
        {
            Debug.Log($"���⸦ ã�� �� ����.{id}");

            return false;
        }

        Debug.Log($" ����id {id}");

        // ��ųʸ��� ���� ���� ź�� ���� ����
        if (!string.IsNullOrEmpty(_currentWeaponId))
        {
            _ammo[_currentWeaponId] = _currentAmmo;
            _totalAmmo[_currentWeaponId] = _currentTotalAmmo;

        }

        // ���� ��ü �� �߻�/ ���� x
        CancelFire();
        CancelReload();
        _isReloading = false;
   
     
        _currentWeapon = weapon;
        _weaponId = id;
        _currentWeaponId = id;

        // ������ �� ��������
        if (_ammo.ContainsKey(id))
        {
            _currentAmmo = _ammo[id];
            _currentTotalAmmo = _totalAmmo[id];
        }


        // ������ �ִ��ġ��
        else
        {
            _currentAmmo = _currentWeapon.MaxAmmo;
            _currentTotalAmmo = _currentWeapon.MaxTotalAmmo;

            _ammo[id] = _currentAmmo;
            _totalAmmo[id] = _currentTotalAmmo;
        }

        // ���� Ȯ�� 
        if (_weaponNames != null)
        {
            for (int i = 0; i < _weaponNames.Count; i++)
            {
                if (_weaponNames[i] == id)
                {
                    _currentWeaponIndex = i;
                    break;
                }

            }

        }

        // ���� �̹��� ���� 
        if (_weaponSpriteRenderer != null && _currentWeapon.WeaponSprite != null)
        {
            _weaponSpriteRenderer.sprite = _currentWeapon.WeaponSprite; 
        }


        Debug.Log($"���� ���� ����: {_currentWeapon.name}");
        return true;
    }

    // ���� ��ü 
    public void SwitchWeapon(int index)
    {
        if (_weaponNames == null || _weaponNames.Count == 0)
        {
            Debug.Log("_weaponNames ����");
            return;
        }

        _currentWeaponIndex += index;

        if (_currentWeaponIndex >= _weaponNames.Count)
        {
            _currentWeaponIndex = 0;
        }
        
        else if (_currentWeaponIndex < 0)
        {
            _currentWeaponIndex = _weaponNames.Count - 1;
        }

        SelectWeapon(_weaponNames[_currentWeaponIndex]);

    }



    // �߻�õ�
    public void TryFire()
    {
        Debug.Log("�߻�");
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

        // ���� ź���� 0���� ������ ����
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

    // ���� ��ġ�� ���� �ѱ� �̹��� ���
    private void Fire()
    {
        // ���콺 ��ġ ���
        Vector2 mousPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint( mousPos );
        mouseWorldPos.z = 0f;

        // ���콺 ���� ���
        Vector3 direction = (mouseWorldPos - _weaponTransform.position).normalized;
        Quaternion targetRotatiom = Quaternion.FromToRotation(Vector3.up, direction);

        // ����
        float t = 1.0f - Mathf.Exp(-_rotationSharpness * Time.deltaTime);
        _weaponTransform.rotation = Quaternion.Slerp(_weaponTransform.rotation, targetRotatiom, t);

        if (_weaponTransform != null)
        {
            Vector3 localScale = _weaponTransform.localScale;

            // ���콺 ��ǥ, �� ��ǥ ��
            if (mouseWorldPos.x < _weaponTransform.position.x)
            {
                localScale.x = 1f; 
            }
            else
            {
                localScale.x = -1f;  
            }
            _weaponTransform.localScale = localScale;
        }


    }

  
    // ���� �õ�
    private void TryReload()
    {
        Debug.Log($"���� ź�� {_currentAmmo} | ���� ź��{_currentTotalAmmo}");
        // ��ȿ�� �˻�
        if (_currentWeapon == null || _currentWeapon.Reload == null) 
        {
            return;
        }

        // �������̸� X
        if (_isReloading)
        {
            return;
        }

        // �ִ� ź�� ������ x
        if (_currentAmmo >= _currentWeapon.MaxAmmo)
        {
            Debug.Log("źâ�� ������");
            return;
        }

        // ź�� ������� ���� �Ұ�
        if (_currentTotalAmmo <= 0)
        {
            return;
        }

        Debug.Log("���� ����");
        CancelFire();
        CancelReload();

        _reloadCTS = new CancellationTokenSource();
        _isReloading = true;

        // ������ �� ������
        CWeaponDataSO weapon = _currentWeapon;

        CWeaponReloadContext reloadContext = new CWeaponReloadContext(
            weapon,
              _currentAmmo,
            LoadComplete
            );

        ReloadAsync(reloadContext, _reloadCTS.Token).Forget();

    }

    // �ݵ� ����
    private void RecoveryRevound()
    {
        if (_weaponTransform == null || _currentWeapon == null)
            {
                return;
            }
        
            _weaponTransform.localPosition = Vector3.Lerp(
                _weaponTransform.localPosition,
                _originPos, 
                Time.deltaTime * _currentWeapon.ReboundSpeed);
        
    }

    private void TrySwitchWeapon()
    {
        // ��ũ�� ���� ��������
        Vector2 scrollValue = Mouse.current.scroll.ReadValue();
        float scrollY = scrollValue.y;


        if (scrollY > 0f)
        {
            SwitchWeapon(-1); 
        }
        else if (scrollY < 0f)
        {
            SwitchWeapon(1);  
        }

        Debug.Log("���⺯��");
    }

    // �ݵ� 
    private void AppltReload()
    {
        if (_weaponTransform == null)
        {
            return;
        }

        _weaponTransform.localPosition = _originPos + (Vector3.down * _currentWeapon.ReboundRange);

        Debug.Log("�ݵ�o");
    }

    private async UniTask ReloadAsync(CWeaponReloadContext context, CancellationToken ct)
    {
        bool success = await _currentWeapon.Reload.ReloadWeapon(context, ct);

        if (success)
        {
            Debug.Log("���� O");
        }

        else
        {
            Debug.Log("���� x");
        }
        _isReloading = false;
    }

    // �߻� �̹��� 
    private async UniTask ReturnFireSprite()
    {
        if (_weaponSpriteRenderer == null)
        {
            return;
        }

        if (_currentWeapon.FireSprite != null)
        {
            _weaponSpriteRenderer.sprite = _currentWeapon.FireSprite;
        }
        float currentFireTime = Time.deltaTime;
        _lastFireTime = currentFireTime;

        await UniTask.Delay(TimeSpan.FromSeconds(_fireSpriteTime), DelayType.UnscaledDeltaTime);

        if (_lastFireTime == currentFireTime)
        {
            _weaponSpriteRenderer.sprite = _currentWeapon.WeaponSprite;
        }


    }

    // �߻� �Ӻ� �÷���
    private async UniTask FireFlashMuzzle()
    {
        if (_muzzleFlash == null) return;

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

    // ���� 
    private void LoadComplete(int ammo)
    {
        if (_currentWeapon == null)
        {
            return;
        }

        int neededAmmo = _currentWeapon.MaxAmmo - _currentAmmo;


        // ź�� ������ ����
        if (_currentWeapon.IsInfiniteAmmo)
        {
            // ������
            _currentAmmo += neededAmmo;
        }
        else
        { 
        int ammoToReload = Mathf.Min(neededAmmo, _currentTotalAmmo);
        _currentAmmo += ammoToReload;
        _currentTotalAmmo -= ammoToReload;
        }


    }



    // ����ü ��������
    private void SpawnProjectile(CWeaponDataSO weapon, Quaternion rotation)
    {
        if (weapon == null || weapon.Projectile == null)
        {
            return;
        }

        // ����
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

        // �Ѿ� ����
        _currentAmmo--;
        Debug.Log($"{projectile.transform.position}");

        AppltReload();

        ReturnFireSprite().Forget();
        FireFlashMuzzle().Forget();
    }



    // ��҉������
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
