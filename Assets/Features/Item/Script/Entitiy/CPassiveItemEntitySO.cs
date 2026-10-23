using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/EntityDataSO/EntityStrategy/PassiveItemEntity", fileName = "PassiveItemEntitySO_")]
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

    public override bool InteractCheck(CEntityController entity)
    {
        return true;
    }

    private void GetItem()
    {
        CItemController.Instance.AddPassiveItem(_itemDataSO);
    }
}
