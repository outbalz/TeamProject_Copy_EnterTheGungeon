using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System;

[CreateAssetMenu(menuName = "Weapon/Reload/CReloadStrategySO", fileName = "CReloadStrategySO")]
public class CReloadStrategySO : CWeaponReloadStrategySO
{
    public override async UniTask<bool> ReloadWeapon(CWeaponReloadContext context, CancellationToken ct)
    {
        if (context.Weapon == null)
        {
            return false;
        }

        float reloadTime = context.Weapon.ReloadTime;

        float duration = Mathf.Max(0.01f, reloadTime);

        await UniTask.Delay(TimeSpan.FromSeconds(duration), DelayType.UnscaledDeltaTime, cancellationToken: ct);

        context.AddAmmo?.Invoke(context.Weapon.MaxAmmo);
        return true;
    }
}
