using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(menuName = "Weapon/Pattern/CStraightPatternSO", fileName = "CStraightPatternSO")]

public class CStraightPatternSO : CWeaponTriggerStrategySO
{
    #region 인스펙터
    [SerializeField] private int _projectilCount = 1;
    #endregion

    public override UniTask TriggerWeapon(CWeaponTriggerContext context, CancellationToken ct)
    {
        //유효성 검사
        if (context == null || context.FirePoint == null || context.SpawnProjectile == null)
        {
            return UniTask.CompletedTask;
        }

        // 검사 (음수 방지)
        int count = Mathf.Max(1, _projectilCount);

        for (int i = 0; i < count; i++)
        {
            ct.ThrowIfCancellationRequested();

            context.SpawnProjectile(context.FirePoint.rotation);

        }

        Debug.Log("StraightPattern");

        return UniTask.CompletedTask;
    }

    protected override void OnValidate()
    {
        // 최소값 보정
        _projectilCount = Mathf.Max(1, _projectilCount);
    }
}