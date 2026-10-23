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
    public int CurrentAmmo { get; private set; }
    public Action<int> AddAmmo { get; private set; }
    #endregion
    public CWeaponReloadContext(CWeaponDataSO weapon, int currentAmmo, Action<int> addAmmo)
    {
        Weapon = weapon;
        CurrentAmmo = currentAmmo;
        AddAmmo = addAmmo;
    }
}



public abstract class CWeaponReloadStrategySO : CGameDataSO
{
    public abstract UniTask<bool> ReloadWeapon(CWeaponReloadContext context, CancellationToken ct);
}
