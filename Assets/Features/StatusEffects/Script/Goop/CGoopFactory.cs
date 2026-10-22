using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoopFactory : CObjectPoolFactory<CGoopFactory, CGoopPool, CGoopController>
{
    protected override void PrefabCheck()
    {
        if (_prefab == null)
        {
            if (CDataBaseManager.Instance.PrefabDB != null)
            {
                if (CDataBaseManager.Instance.PrefabDB.TryGet("Goop", out CPrefabDataSO prefabDataSO))
                {
                    if (prefabDataSO is CGoopPrefabDataSO dataSO)
                    {
                        _prefab = dataSO.Prefab;
                    }
                }
            }

        }

        base.PrefabCheck();
    }
}
