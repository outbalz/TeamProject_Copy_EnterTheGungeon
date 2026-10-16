using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;


[CreateAssetMenu(menuName = "Weapon/ProjectileSettings/CProjectilePhysicsSO", fileName = "CProjectilePhysicsSO")]

public class CProjectilePhysicsSO : ScriptableObject
{
    #region 인스펙터
    [Header("도탄 설정")]
    [SerializeField] private int _bounceCount = 0;

    [Header("관통 설정")]
    [SerializeField] private int _pierceCount = 0;

    [Header("넉백 설정")]
    [SerializeField] private float _knockback = 0f;

    #endregion

    #region 프로퍼티 
    public int BounceCount => _bounceCount;
    public int pierceCount => _pierceCount;
    public float Knockback => _knockback;
    #endregion



}
