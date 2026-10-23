using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Projectile", fileName = "CProjectileDataSO")]
public abstract class CProjectileDataSO : CGameDataSO
{
    #region 인스펙터
    [Header("발사체 설정")]
    [SerializeField] private float _projectileSpeed = 5f;
    [SerializeField] private float _projectileScale = 1f;
    [SerializeField] private float _lifeTime = 5f;

    [Header("발사 구조")]
    [SerializeField] private CProjectilePhysicsSO _ProjectilePhysics = null;
    [SerializeField] private CProjectileEffectSO _ProjectileEffect = null;
    #endregion

    #region 프로퍼티 
    public float ProjectileSpeed => _projectileSpeed;
    public float ProjectileScale => _projectileScale;
    public float LifeTime => _lifeTime;
    public CProjectilePhysicsSO ProjectilePhysics => _ProjectilePhysics;
    public CProjectileEffectSO Effect => _ProjectileEffect;
    #endregion

    //투사체가 벽이나 오브젝트에 충돌했을 경우 호출
    public abstract UniTask OnImpact();

    //투사체가 케릭터에게 맞았을 때 호출
    public abstract UniTask OnHit();
}
