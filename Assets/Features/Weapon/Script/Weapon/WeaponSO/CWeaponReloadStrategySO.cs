using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class CWeaponReloadContext
{
    #region 프로퍼티
    public CWeaponDataSO Weapon { get; private set; }
    public Action ReloadComplete { get; private set; }

    #endregion
    public CWeaponReloadContext(CWeaponDataSO weapon, Action reloadComplete)
    {
        Weapon = weapon;
        ReloadComplete = reloadComplete;
    }
}



public abstract class CWeaponReloadStrategySO : CGameDataSO
{
    public abstract UniTask ReloadWeapon(CWeaponReloadContext context, CancellationToken ct);
}
