using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;


[CreateAssetMenu(menuName = "Weapon/Pattern/CSpreadPatternSO", fileName = "CSpreadPatternSO")]

public class CSpreadPatternSO : CWeaponTriggerStrategySO
{
    #region 인스펙터
    [SerializeField] private int _projectilCount = 3;
    [SerializeField] private float _spreadAngle = 10f;
    #endregion

    public override UniTask TriggerWeapon(CWeaponTriggerContext context, CancellationToken ct)
    {
        //유효성 검사
        if (context == null || context.FirePoint == null || context.SpawnProjectile == null)
        {
            return UniTask.CompletedTask;
        }

        ct.ThrowIfCancellationRequested();

        // 검사 (음수 방지)
        int count = Mathf.Max(1, _projectilCount);

        // _projectilCount가 1발인 경우 
        if (count == 1)
        {
            context.SpawnProjectile(context.FirePoint.rotation);

            return UniTask.CompletedTask;
        }

        for (int i = 0; i < count; i++)
        {
            ct.ThrowIfCancellationRequested();

            float t = (float)i / (count - 1);

            // 산탄 비율
            float angle = Mathf.Lerp(-_spreadAngle * 0.5f, _spreadAngle * 0.5f, t);

            // 최종 계산 비율
            Quaternion rotation = context.FirePoint.rotation * Quaternion.Euler(0f, angle, 0f);

            context.SpawnProjectile(rotation);

        }

        Debug.Log("SpreadPattern");

        return UniTask.CompletedTask;
    }

    protected override void OnValidate()
    {
        // 최소값 보정
        _projectilCount = Mathf.Max(1, _projectilCount);
        _spreadAngle = Mathf.Max(0f, _spreadAngle);
    }
}