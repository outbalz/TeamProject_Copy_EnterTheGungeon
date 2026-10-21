using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CItemCoolDownStrategySO : CGameDataSO
{
    public abstract bool OnItemUse(CActiveItem item);
}
