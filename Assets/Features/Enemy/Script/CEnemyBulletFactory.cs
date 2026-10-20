using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyBulletFactory : CObjectPoolFactory<CEnemyBullet, CEnemyBulletPool, CEnemyBullet>
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
    
    protected override void PoolCheck()
    {
        if (_pool == null)
        {
            _pool = CEnemyBulletPool.Instance;
        }

        base.PoolCheck();
    }

    public CEnemyBullet Creat(Vector2 pos, Quaternion rot)
    {
        CEnemyBullet bullet = _pool.GetFromPool();

        bullet.transform.position = pos;
        bullet.transform.rotation = rot;

        return bullet;
    }

}
