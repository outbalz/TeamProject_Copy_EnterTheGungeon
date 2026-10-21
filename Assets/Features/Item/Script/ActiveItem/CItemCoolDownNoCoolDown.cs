using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemCoolDownNoCoolDown : CItemCoolDownStrategySO
{
    public override bool OnItemUse(CActiveItem item)
    {
        return true;
    }
}
