using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/ItemDataSO/ActiveItemDataSO", fileName = "ActiveItemDataSO_")]
public class CActiveItemDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] protected CActiveItemEffectStrategySO[] _effect;
    [SerializeField] protected CItemCoolDownStrategySO _coolDown;
    //사용 횟수, -1때 무한
    [SerializeField] protected int _useAmount = -1;
    [SerializeField] protected CEntityDataSO _itemEntitySO;
    #endregion

    #region 프로퍼티
    public CActiveItemEffectStrategySO[] Effect => _effect;
    public CItemCoolDownStrategySO ColDown => _coolDown;
    public int UseAmount => _useAmount;
    public CEntityDataSO ItemEntitySO => _itemEntitySO;
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
