using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/PrefabDataSO/GoopPrefabDataSO", fileName = "GoopPrefabDataSO")]
public class CGoopPrefabDataSO : CPrefabDataSO
{
    #region 인스팩터
    [SerializeField] private CGoopController _prefab;
    #endregion

    #region 프로퍼티
    public CGoopController Prefab => _prefab;
    #endregion
}

