using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

public enum PropertyType
{
    None,
    Fire,
    Electric,
    Ice,
    Boom
}

[CreateAssetMenu(menuName = "Weapon/ProjectileSettings/CProjectileEffectSO", fileName = "CProjectileEffectSO")]

public abstract class CProjectileEffectSO : ScriptableObject
{
    #region 인스펙터
    [Header("이펙트 및 사운드")]
    [SerializeField] private GameObject _impactPrefab = null;
    [SerializeField] private AudioClip _impactSound = null;
    [SerializeField] private PropertyType _propertyType = PropertyType.None;
    #endregion

    #region 프로퍼티 
    public GameObject ImpactPrefab => _impactPrefab;
    public AudioClip ImpactSound => _impactSound;
    public PropertyType PropertyType => _propertyType;
    #endregion

    public abstract void ApplyEffect(GameObject target);
   

}
