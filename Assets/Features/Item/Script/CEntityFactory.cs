using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEntityFactory : CFactory<CEntityController>
{
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
        entity.Rb.AddForce(velocity, ForceMode2D.Impulse);
        return entity;
    }
}
