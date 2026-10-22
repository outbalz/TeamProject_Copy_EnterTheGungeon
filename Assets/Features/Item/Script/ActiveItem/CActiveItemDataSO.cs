using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/ItemDataSO/ActiveItemDataSO", fileName = "ActiveItemDataSO_")]
public class CActiveItemDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] protected CActiveItemEffectStrategySO[] _effect;
    [SerializeField] protected CItemCoolDownStrategySO _coolDown;
    [SerializeField] protected int _useAmount = -1;
    #endregion

    #region 프로퍼티
    public CActiveItemEffectStrategySO[] Effect => _effect;
    public CItemCoolDownStrategySO ColDown => _coolDown;
    public int UseAmount => _useAmount;
    #endregion

    public bool OnItemUes(CActiveItem item)
    {
        if (_coolDown.OnItemUse(item))
        {
            for (int i = 0; i < _effect.Length; i++)
            {
                _effect[i].OnUse();
            }
            return true;
        }

        return false;
    }
}
