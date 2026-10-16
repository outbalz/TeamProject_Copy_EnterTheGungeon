using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;



public class CWeaponTriggerContext
{
    #region 인스펙터

    #endregion

    #region 프로퍼티
    public CWeaponDataSO Weapon { get; private set; }
    public Transform FirePoint { get; private set; }
    public Action<Quaternion> SpawnProjectile { get; private set; }
    #endregion

    public CWeaponTriggerContext(CWeaponDataSO weapon, Transform firePoint, Action<Quaternion> spawnProjectile)
    {
        Weapon = weapon;
        FirePoint = firePoint;
        SpawnProjectile = spawnProjectile;
    }


}

public abstract class CWeaponTriggerStrategySO : CGameDataSO
{
 

    public abstract UniTask TriggerWeapon(CWeaponTriggerContext context, CancellationToken ct);
}
