using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActiveItemDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] protected CActiveItemEffectStrategySO _effect;
    #endregion

    #region 프로퍼티
    public CActiveItemEffectStrategySO Effect => _effect;
    #endregion
}
