using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

[CreateAssetMenu(menuName = "FireMode/CChargingFiringStrategySO", fileName = "CChargingFiringStrategySO")]
public class CChargingFiringStrategySO : CWeaponStrategy
{
    #region 인스펙터
    [SerializeField] private float _chargingInterval = 1f;
    #endregion

    public override async UniTask FireAsync(CWeaponFireContext context, CancellationToken ct)
    {
        //유효성 검사
        if (context == null || context.FirePoint == null || context.SpawnProjectile == null)
        {
            return;
        }

        // 검사 (음수 방지)
        float Interval = Mathf.Max(0.01f, _chargingInterval);
        // 소한
        context.SpawnProjectile(context.FirePoint.rotation);
        // 딜레이
        await UniTask.Delay(TimeSpan.FromSeconds(Interval), DelayType.UnscaledDeltaTime, cancellationToken: ct);

        ct.ThrowIfCancellationRequested();
    }
}
