using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEntityFactory : CFactory<CEntityFactory, CEntityController>
{
    protected override void PrefabCheck()
    {
        if (_prefab == null)
        {
            if (CDataBaseManager.Instance.PrefabDB != null)
            {
                if (CDataBaseManager.Instance.PrefabDB.TryGet("Entity", out CPrefabDataSO prefabDataSO))
                {
                    if(prefabDataSO is CEntityPrefabDataSO dataSO)
                    {
                        _prefab = dataSO.Prefab; 
                    }
                }
            }

        }

        base.PrefabCheck();
    }

    public CEntityController Creat(CEntityDataSO data)
    {
        CEntityController entity = Creat();
        entity.InitializeEntitiy(data);
        return entity;
    }

    public CEntityController CreatAndTrow(Vector2 velocity, CEntityDataSO data)
    {
        CEntityController entity = Creat();
        entity.InitializeEntitiy(data);

        if (velocity != Vector2.zero && entity.Rb != null)
        {
            entity.Rb.AddForce(velocity, ForceMode2D.Impulse);
        }

        return entity;
    }
}
