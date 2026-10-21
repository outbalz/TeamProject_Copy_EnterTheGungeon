using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyBulletPool : CObjectPool<CEnemyBulletPool, CEnemyBullet>
{
    protected override void PrefabCheck()
    {
        if (_prefab == null)
        {
            if (CDataBaseManager.Instance.PrefabDB != null)
            {
                if (CDataBaseManager.Instance.PrefabDB.TryGet("EnemyBullet", out CPrefabDataSO prefabDataSO))
                {
                    if (prefabDataSO is CEnemyBulletPrefabDataSO dataSO)
                    {
                        _prefab = dataSO.Prefab;
                    }
                }
            }

        }

        base.PrefabCheck();
    }
}
