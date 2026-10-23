using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/PrefabDataSO/CProjectilePrefabDataSO", fileName = "CProjectilePrefabDataSO")]
public class CProjectilePrefabDataSO : CPrefabDataSO
{
    #region 인스펙터
    [SerializeField] private ProjectileController _projectilePrefab;
    #endregion

    #region 프로퍼티
    public ProjectileController ProjectilePrefab => _projectilePrefab;
    #endregion
}
