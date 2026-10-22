using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CEntityConsumeOnGetSO : CEntityStrategySO
{
    public override void InitializeEntitiy(CEntityController entity)
    {
        entity.OnInteractEvent += GetItem;
        entity.OnInteractEvent += () => Destroy(entity.gameObject);
    }

    protected abstract void GetItem();
}
