using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemCoolDownDamageDealtSO : CItemCoolDownStrategySO
{
    #region 인스팩터
    [SerializeField] private float _requirDamage;
    #endregion


    public override bool OnItemUse(CActiveItem item)
    {
        if(item.Cooldown > 0)
        {
            return false;
        }

        item.Cooldown = _requirDamage;
        return true;
    }

    public void OnDealtDamage(CActiveItem item, float damage)
    {
        item.Cooldown -= damage;
    }
}
