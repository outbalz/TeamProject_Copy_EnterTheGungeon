using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStatusEffectType
{
    Player,
    Enemy,
    Goop
}


public class CStatusEffectSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] private EStatusEffectType _type;
    [SerializeField] private bool _isStackable;
    [SerializeField] private CStatusEffectStrategySO _strategy;
    #endregion

    #region 프로퍼티
    public EStatusEffectType EffectType => _type;
    public bool IsStackable => _isStackable;
    public CStatusEffectStrategySO Strategy => _strategy;
    #endregion


}
