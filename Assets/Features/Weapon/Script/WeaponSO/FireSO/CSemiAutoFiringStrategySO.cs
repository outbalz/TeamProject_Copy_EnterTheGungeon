using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

[CreateAssetMenu(menuName = "Weapon/FireMode/CSemiAutoFiringStrategySO", fileName = "CSemiAutoFiringStrategySO")]

public class CSemiAutoFiringStrategySO : CWeaponTriggerStrategySO
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

        // 검사 (음수 방지)
        float Interval = Mathf.Max(0.01f, _shotInterval);
        // 소한
        await context.Weapon.Pattern.TriggerWeapon(context, ct);
           

        Debug.Log("SemiAutoFiring");

        // 딜레이
        await UniTask.Delay(TimeSpan.FromSeconds(Interval), DelayType.UnscaledDeltaTime, cancellationToken: ct);

        ct.ThrowIfCancellationRequested();
    }

    protected override void OnValidate()
    {
        // 최소값 보정
        _shotInterval = Mathf.Max(0f, _shotInterval);
    }
}
