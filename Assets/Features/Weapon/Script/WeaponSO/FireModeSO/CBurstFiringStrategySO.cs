using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

[CreateAssetMenu(menuName = "FireMode/CBurstFiringStrategySO", fileName = "CBurstFiringStrategySO")]
public class CBurstFiringStrategySO : CWeaponStrategy
{
    #region 인스펙터
    [SerializeField] private float _shotInterval = 0.1f;
    [SerializeField] private int _shotCount = 0;
    #endregion

    public override async UniTask FireAsync(CWeaponFireContext context, CancellationToken ct)
    {
        //유효성 검사
        if (context == null || context.FirePoint == null || context.SpawnProjectile == null)
        {
            return;
        }

        // 검사 (음수 방지)
        float Interval = Mathf.Max(0.01f, _shotInterval);

        for (int i = 0; i < _shotCount; i++)
        {
            // 소한
            context.SpawnProjectile(context.FirePoint.rotation);
           
            ct.ThrowIfCancellationRequested();

            // 마지막발떄는 딜레이 x
            if (i >= _shotCount -1)
            {
                continue;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(Interval), DelayType.UnscaledDeltaTime, cancellationToken: ct);

        }
    }
}
