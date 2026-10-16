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
    #endregion

    #region 내부 변수
    private CWeaponDataSO _currentWeapon;
    private CancellationTokenSource _fireCTS;
    #endregion

    #region 프로퍼티
    public CWeaponDataSO CurrentWeapon => _currentWeapon;
    #endregion

    private void Start()
    {
        SelectWeapon(_weaponId);
    }
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryFire();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            CancleFire();
        }
        
    }


    private void OnDestroy()
    {
        CancleFire();
    }

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

        _currentWeapon = weapon;
        _weaponId = id;
        return true;
    }

    public void TryFire()
    {
        if (_currentWeapon == null)
        {
            return;
        }

        if (_firePoint == null)
        {
            return;
        }

        if (_currentWeapon.Trigger == null || _currentWeapon.Projectile == null)
        {
            return;
        }

        CancleFire();
        _fireCTS = new CancellationTokenSource();

        // 무기 변경
        CWeaponDataSO weapon = _currentWeapon;

        CWeaponTriggerContext context = new CWeaponTriggerContext(
            weapon,
            _firePoint,
            (Rotate) => SpawnProjectile(weapon, Rotate
            ));

        weapon.Trigger.TriggerWeapon(context, _fireCTS.Token).Forget();
    }

    private void SpawnProjectile(CWeaponDataSO weapon, Quaternion rotation)
    {
        if (weapon == null || weapon.Projectile == null)
        {
            return;
        }

        ProjectileController projectile = CProjectileFactory.Instance.Creat(_firePoint);

        if (projectile == null )
        {
            return;
        }


        if (projectile != null)
        {
            projectile.transform.position = _firePoint.position;
            projectile.transform.rotation = rotation;

            projectile.gameObject.SetActive(true);

            projectile.SetUp(weapon.Projectile);

        }

    }

    private void CancleFire()
    {
        if (_fireCTS == null)
        {
            return;
        }

        _fireCTS.Cancel();
        _fireCTS.Dispose();

        _fireCTS = null;

    }


}
