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
    [SerializeField] private float _reload = 2f;

    [Header("발사 구조")]
    [SerializeField] private CWeaponTriggerContext _fireMode = null;
    [SerializeField] private CWeaponTriggerContext _pattern = null;

    [Header("투사체 연결")]
    [SerializeField] private CProjectileDataSO _projectile = null;

    [SerializeField] CWeaponTriggerStrategySO _trigger;
    #endregion

    #region 프로퍼티
    public CWeaponTriggerStrategySO Trigger => _trigger;
    public string WeaponName => _weaponName;
    public int damage => _damage;
    public float reload => _reload;
    public CWeaponTriggerContext FireMode => _fireMode;
    public CWeaponTriggerContext Pattern => _pattern;
    public CProjectileDataSO Projectile => _projectile;
    #endregion



}
