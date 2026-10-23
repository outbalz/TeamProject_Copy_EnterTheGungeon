using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;


[CreateAssetMenu(menuName = "Weapon/FireMode/CAutoFiringStrategySO", fileName = "CAutoFiringStrategySO")]

public class CAutoFiringStrategySO : CWeaponTriggerStrategySO
{
    #region 인스펙터
    [SerializeField] private float _shotInterval = 0.1f;
    #endregion

    public override async UniTask TriggerWeapon(CWeaponTriggerContext context, CancellationToken ct)
    {
        //유효성 검사
        if (context == null || context.FirePoint == null || context.SpawnProjectile == null)
        {
            return;
        }

        while (true)
        {
            // 검사 (음수 방지)
            float Interval = Mathf.Max(0.01f, _shotInterval);

            if (context.Weapon.Pattern != null)
            {
                await context.Weapon.Pattern.TriggerWeapon(context, ct);
            }

            else
            {
                // 소한
                context.SpawnProjectile(context.FirePoint.rotation);
            }
            Debug.Log("AutoFiring");

            // 딜레이
            await UniTask.Delay(TimeSpan.FromSeconds(Interval), DelayType.UnscaledDeltaTime, cancellationToken: ct);

            ct.ThrowIfCancellationRequested();
        }

    }

    protected override void OnValidate()
    {
        // 최소 값 보정
        _shotInterval = Mathf.Max(0.01f, _shotInterval);
    }

}
