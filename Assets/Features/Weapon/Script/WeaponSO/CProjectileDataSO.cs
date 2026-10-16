using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Weapon/Projectile", fileName = "CProjectileDataSO")]
public class CProjectileDataSO : ScriptableObject
{
    #region 인스펙터
    [Header("발사체 설정")]
    [SerializeField] private GameObject _projectilePrefab = null;
    [SerializeField] private float _projectileSpeed = 5f;
    [SerializeField] private float _projectileScale = 1f;

    [Header("발사 구조")]
    [SerializeField] private CProjectilePhysicsSO _ProjectilePhysics = null;
    #endregion

    #region 프로퍼티 
    public GameObject ProjectilePrefab => _projectilePrefab;
    public float ProjectileSpeed => _projectileSpeed;
    public float ProjectileScale => _projectileScale;
    public CProjectilePhysicsSO ProjectilePhysics => _ProjectilePhysics;
    #endregion



}
