using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CWeaponFireContext
{
    #region
    public CWeaponDataSO Weapon { get; private set; }
    public Transform FirePoint { get; private set; }
    public Action<Quaternion> SpawnProjectile { get; private set; }
    #endregion

    public CWeaponFireContext (CWeaponDataSO weapon, Transform firePoint, Action<Quaternion> spawnProjectile)
    {
        Weapon = weapon;
        FirePoint = firePoint;
        SpawnProjectile = spawnProjectile;
    }


}
public abstract class CWeaponStrategy : ScriptableObject
{
    public abstract UniTask FireAsync(CWeaponFireContext context, CancellationToken ct);
}
