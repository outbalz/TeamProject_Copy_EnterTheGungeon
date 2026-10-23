using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Weapon", fileName = "Weapon")]
public class CWeaponDataSO : CGameDataSO
{
    #region 인스팩터
    [Header("총기 설정")]
    [SerializeField] private string _weaponName = "Weapon";
    [SerializeField] private int _damage = 10;

    [Header("스프라이트 설정")]
    [SerializeField] private Sprite _weaponSprite;
    [SerializeField] private Sprite _fireSprite;

    [Header("머즐 설정")]
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private float _muzzleFlashTime = 0.08f;

    [Header("반동 설정")]
    [SerializeField] private float _reboundRange = 0.2f;
    [SerializeField] private float _reboundSpeed = 10f;

    [Header("재장전")]
    [SerializeField] private int _maxAmmo = 10;  // 최대 탄수
    [SerializeField] private int _maxTotalAmmo = 120; // 총 소지 탄수
    [SerializeField] private float _reloadTime = 2f;
    [SerializeField] private bool _isInfiniteAmmo = false;


    [Header("투사체 연결")]
    [SerializeField] private CProjectileDataSO _projectile;
    [SerializeField] private CWeaponReloadStrategySO _reload;
    [SerializeField] private CWeaponTriggerStrategySO _trigger;
    [SerializeField] private CWeaponTriggerStrategySO _pattern;
    #endregion

    #region 프로퍼티
    public CWeaponTriggerStrategySO Trigger => _trigger;
    public CWeaponTriggerStrategySO Pattern => _pattern;
    public string WeaponName => _weaponName;
    public int Damage => _damage;
    public Sprite WeaponSprite => _weaponSprite;
    public Sprite FireSprite => _fireSprite;
    public GameObject MuzzleFlash => _muzzleFlash;
    public float MuzzleFlashTime => _muzzleFlashTime;
    public float ReboundRange => _reboundRange;
    public float ReboundSpeed => _reboundSpeed;
    public int MaxAmmo => _maxAmmo;
    public int MaxTotalAmmo => _maxTotalAmmo;
    public float ReloadTime => _reloadTime;
    public bool IsInfiniteAmmo => _isInfiniteAmmo;
    public CWeaponReloadStrategySO Reload => _reload;
    public CProjectileDataSO Projectile => _projectile;
    #endregion



}
