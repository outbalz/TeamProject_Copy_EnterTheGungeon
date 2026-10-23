using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;



[CreateAssetMenu(menuName = "Weapon/FireMode/CChargingFiringStrategySO", fileName = "CChargingFiringStrategySO")]

public class CChargingFiringStrategySO : CWeaponTriggerStrategySO
{
    #region 인스펙터
    [SerializeField] private float _minCharge = 0.1f;
    [SerializeField] private float _maxCharge = 2f;
    #endregion

    public override async UniTask TriggerWeapon(CWeaponTriggerContext context, CancellationToken ct)
    {
        //유효성 검사
        if (context == null || context.FirePoint == null || context.SpawnProjectile == null)
        {
            return;
        }

        // 검사 (음수 방지)
        float minCharge = Mathf.Max(0.01f, _minCharge);
        float maxCharge = Mathf.Max(0.01f, _maxCharge);

        float currentChargeTime = 0f;

        // 차징 시간 계산
        while (currentChargeTime < maxCharge)
        {
            await UniTask.Yield(cancellationToken: ct);

            ct.ThrowIfCancellationRequested();

            currentChargeTime += Time.deltaTime;

            Debug.Log("Charging");
        }

        // 최소 차징 보정
        if (currentChargeTime < minCharge)
        {
            return;
        }

        // 차징 비율 계산
        float chargeRatio = Mathf.InverseLerp(minCharge, maxCharge, currentChargeTime);
        chargeRatio = Mathf.Clamp01(chargeRatio);

        // 소한
        await context.Weapon.Pattern.TriggerWeapon(context, ct);

        Debug.Log("ChargingFiring");

    }

    protected override void OnValidate()
    {
        // 최소값 보정
        _minCharge = Mathf.Max(0f, _minCharge);
        _maxCharge = Mathf.Max(0f, _maxCharge);
    }
}
