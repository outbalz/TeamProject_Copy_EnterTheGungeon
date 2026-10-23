using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System;

[CreateAssetMenu(menuName = "Weapon/Reload/CHeatReloadStrategySO", fileName = "CHeatReloadStrategySO")]
public class CHeatReloadStrategySO : CWeaponReloadStrategySO
{
    [SerializeField] private float _coolTime = 2f;
    public override async UniTask<bool> ReloadWeapon(CWeaponReloadContext context, CancellationToken ct)
    {
        if (context.Weapon == null)
        {
            return false;
        }

        float duration = Mathf.Max(0.01f, _coolTime);

      
            // 장전 딜래이
            await UniTask.Delay(TimeSpan.FromSeconds(duration), DelayType.UnscaledDeltaTime, cancellationToken: ct);

            // 장전 (열 초기화)
            context.AddAmmo?.Invoke(0); 
        

        return true;
    }
}
