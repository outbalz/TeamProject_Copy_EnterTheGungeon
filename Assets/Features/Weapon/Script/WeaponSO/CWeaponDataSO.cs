using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Rendering;

[CreateAssetMenu (menuName = "Weapon", fileName = "Weapon")]
public class CWeaponDataSO : ScriptableObject
{
    #region 인스펙터
    [Header("총기 설정")]
    [SerializeField] private string _weaponName = "Weapon";
    [SerializeField] private int _damage = 10;

    [Header("발사 구조")]
    [SerializeField] private CWeaponStrategy _firestategy = null;
    [SerializeField] private CWeaponStrategy _pattern = null;

    [Header("투사체 연결")]
    [SerializeField] private CProjectileDataSO _projectile = null;
    #endregion

    #region 프로퍼티 
    public string WeaponName => _weaponName;
    public int damage => _damage;
    public CWeaponStrategy Firestategy => _firestategy;
    public CWeaponStrategy Pattern => _pattern;
    public CProjectileDataSO Projectile => _projectile;
    #endregion



}
