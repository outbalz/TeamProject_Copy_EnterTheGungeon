using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

[CreateAssetMenu (menuName = "Weapon", fileName = "Weapon")]
public class CWeaponDataSO : ScriptableObject
{
    #region 인스펙터
    [Header("총기 설정")]
    [SerializeField] private string _weaponName = "Weapon";
    [SerializeField] private int _damage = 10;

    [Header("발사체 설정")]
    [SerializeField] private GameObject _projectilePrefab = null;
    [SerializeField] private float _projectileSpeed = 5f;
    [SerializeField] private float _projectileScale = 1f;

    
    [SerializeField] private CWeaponStrategy _firestategy = null;
    #endregion

    #region 프로퍼티 
    public string WeaponName => _weaponName;
    public int damage => _damage;

    public GameObject ProjectilePrefab => _projectilePrefab;

    public float ProjectileSpeed => _projectileSpeed;

    public float ProjectileScale => _projectileScale;

    public CWeaponStrategy Firestategy => _firestategy;
    #endregion



}
