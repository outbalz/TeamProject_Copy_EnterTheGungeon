using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/PrefabDataSO/EnemyBulletPrefabDataSO", fileName = "EnemyBulletPrefabDataSO")]
public class CEnemyBulletPrefabDataSO : CPrefabDataSO
{
    #region 인스팩터
    [SerializeField] private CEnemyBullet _prefab;
    #endregion

    #region 프로퍼티
    public CEnemyBullet Prefab => _prefab;
    #endregion
}
