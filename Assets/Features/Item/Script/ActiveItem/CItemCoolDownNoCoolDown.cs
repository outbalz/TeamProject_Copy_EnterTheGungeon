using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/ItemDataSO/ActiveItemCoolDown/NoCoolDown", fileName = "NoCoolDownSO")]
public class CItemCoolDownNoCoolDown : CItemCoolDownStrategySO
{
    public override bool OnItemUse(CActiveItem item)
    {
        return true;
    }
}
