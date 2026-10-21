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
    [SerializeField] private Sprite _weaponSprite;

    [Header("재장전")]
    [SerializeField] private int _maxAmmo = 10;  // 최대 탄수
    [SerializeField] private int _maxTotalAmmo = 120; // 총 소지 탄수
    [SerializeField] private float _reloadTime = 2f;


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
    public int MaxAmmo => _maxAmmo;
    public int MaxTotalAmmo => _maxTotalAmmo;
    public float ReloadTime => _reloadTime;
    public CWeaponReloadStrategySO Reload => _reload;
    public CProjectileDataSO Projectile => _projectile;
    #endregion



}
