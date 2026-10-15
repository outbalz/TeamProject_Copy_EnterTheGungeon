using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(menuName = "Pattern/CSpreadPatternSO", fileName = "CSpreadPatternSO")]
public class CSpreadPatternSO : CWeaponStrategy
{
    #region 인스펙터
    [SerializeField] private int _projectilCount = 3;
    [SerializeField] private float _spreadAngle = 10f;
    #endregion

    public override UniTask FireAsync(CWeaponFireContext context, CancellationToken ct)
    {
        if (context == null || context.FirePoint == null || context.SpawnProjectile == null)
        {
            return UniTask.CompletedTask;
        }

        ct.ThrowIfCancellationRequested();

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

            float angle = Mathf.Lerp(-_spreadAngle * 0.5f, _spreadAngle * 0.5f, t);

            Quaternion rotation = context.FirePoint.rotation * Quaternion.Euler(0f, angle, 0f);

            context.FirePoint.rotation = rotation;

        }

        return UniTask.CompletedTask;
    }

    private void OnValidate()
    {
        // 최소값 보정
        _projectilCount = Mathf.Max(1, _projectilCount);
        _spreadAngle = Mathf.Max(0f, _spreadAngle);
    }
}