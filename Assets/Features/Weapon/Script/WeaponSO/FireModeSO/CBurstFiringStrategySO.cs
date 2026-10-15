using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

[CreateAssetMenu(menuName = "Weapon/FireMode/CBurstFiringStrategySO", fileName = "CBurstFiringStrategySO")]

public class CBurstFiringStrategySO : CWeaponStrategy
{
    #region 인스펙터
    [SerializeField] private float _shotInterval = 0.1f;
    [SerializeField] private int _shotCount = 1;
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
        int count = Mathf.Max(1, _shotCount);

        for (int i = 0; i < count; i++)
        {
            // 소한
            context.SpawnProjectile(context.FirePoint.rotation);

            Debug.Log("BurstFiring");

            ct.ThrowIfCancellationRequested();

            // 마지막발떄는 딜레이 x
            if (i >= count - 1)
            {
                continue;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(Interval), DelayType.UnscaledDeltaTime, cancellationToken: ct);

        }
    }

    private void OnValidate()
    {
        // 최소 값 보정
        _shotInterval = Mathf.Max(0.01f, _shotInterval);
        _shotCount = Mathf.Max(1, _shotCount);
    }
}
