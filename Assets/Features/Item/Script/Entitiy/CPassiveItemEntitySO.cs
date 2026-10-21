using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPassiveItemEntitySO : CEntityStrategySO
{
    #region 인스팩터
    [SerializeField] private CPassiveItemDataSO _itemDataSO;
    #endregion

    public override void InitializeEntitiy(CEntityController entity)
    {
        entity.OnInteractEvent += GetItem;
        entity.OnInteractEvent += () => Destroy(entity.gameObject);
    }

    private void GetItem()
    {
        CItemController.Instance.AddPassiveItem(_itemDataSO);
    }
}
