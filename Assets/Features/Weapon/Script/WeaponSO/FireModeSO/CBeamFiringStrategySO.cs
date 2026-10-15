using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

[CreateAssetMenu(menuName = "FireMode/CBeamFiringStrategySO", fileName = "CBeamFiringStrategySO")]
public class CBeamFiringStrategySO : CWeaponStrategy
{

    public override async UniTask FireAsync(CWeaponFireContext context, CancellationToken ct)
    {
        //유효성 검사
        if (context == null || context.FirePoint == null || context.SpawnProjectile == null)
        {
            return;
        }

        // 소환
        context.SpawnProjectile(context.FirePoint.rotation);

        while (true)
        {
            await UniTask.Yield(cancellationToken: ct);


            Debug.Log("BeamFiring");

            ct.ThrowIfCancellationRequested();
        }
    }
}
