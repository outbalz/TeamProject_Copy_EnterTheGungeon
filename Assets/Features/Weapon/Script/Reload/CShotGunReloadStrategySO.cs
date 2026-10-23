using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System;

[CreateAssetMenu(menuName = "Weapon/Reload/CShotGunReloadStrategySO", fileName = "CShotGunReloadStrategySO")]
public class CShotGunReloadStrategySO : CWeaponReloadStrategySO
{
    [SerializeField] private float _singleReloadTime = 0.4f;
    public override async UniTask<bool> ReloadWeapon(CWeaponReloadContext context, CancellationToken ct)
    {
       if (context.Weapon == null)
        {
            return false;
        }

        float duration = Mathf.Max(0.01f, _singleReloadTime);

        while (true)
        {
            // 장전 딜래이
            await UniTask.Delay(TimeSpan.FromSeconds(duration), DelayType.UnscaledDeltaTime, cancellationToken: ct);

            // 장전 (1발씩)
            context.AddAmmo?.Invoke(1);

            //현재 탄이 최대 탄 넘으면 break
            if (context.Weapon != null && context.CurrentAmmo >= context.Weapon.MaxAmmo)
            {
                break;
            }
        }

        return true;
    }
}
