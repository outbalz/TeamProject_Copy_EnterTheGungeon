using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActiveItemEntitySO : CEntityStrategySO
{
    #region 인스팩터
    [SerializeField] private CActiveItemDataSO _itemDataSO;
    #endregion

    public override void InitializeEntitiy(CEntityController entity)
    {
        entity.OnInteractEvent += GetItem;
        entity.OnInteractEvent += () => Destroy(entity.gameObject);
    }

    public override bool InteractCheck(CEntityController entity)
    {
        return true;
    }

    private void GetItem()
    {
        CItemController.Instance.AddActiveItem(_itemDataSO);
    }
}
