using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponFactory : CFactory<CWeaponFactory, WeaponController>
{
    protected override void PrefabCheck()
    {
        // _prefab 없을 경우 방어코드 
        if (_prefab == null)
        {
            if (CDataBaseManager.Instance.PrefabDB != null)
            {
                if (CDataBaseManager.Instance.PrefabDB.TryGet("Projectile", out CPrefabDataSO PrefabDataSO))
                {
        
                    if (PrefabDataSO is CProjectilePrefabDataSO dataSO)
                    {
                      
                    }
                }
            }
        }
        base.PrefabCheck();
    }

    public override WeaponController Creat()
    {
        return base.Creat();
    }




}
