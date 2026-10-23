using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(menuName = "Weapon/Pattern/CCirclePatternStrategySO", fileName = "CCirclePatternStrategySO")]
public class CCirclePatternStrategySO : CWeaponTriggerStrategySO
{
    #region 인스펙터
    [SerializeField] private int _projectilCount = 3;
    #endregion

    public override  UniTask TriggerWeapon(CWeaponTriggerContext context, CancellationToken ct)
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
        // 카운트에 따른 각도
        float angleRatio = 360f / count;

        for (int i = 0; i < count; i++)
        {
            ct.ThrowIfCancellationRequested();

            // 최종 각도
            float angle = angleRatio * i;

            // 각도 계산
            Quaternion rotation = context.FirePoint.rotation * Quaternion.Euler(0f, 0f, angle);

            context.SpawnProjectile(rotation);

        }

        Debug.Log("CirclePattern");

        return UniTask.CompletedTask;
    }

    protected override void OnValidate()
    {
        // 최소값 보정
        _projectilCount = Mathf.Max(1, _projectilCount);
    }
}