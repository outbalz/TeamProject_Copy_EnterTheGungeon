using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;



[CreateAssetMenu(menuName = "Weapon/Pattern/CSpiralPatternSO", fileName = "CSpiralPatternSO")]

public class CSpiralPatternSO : CWeaponStrategy
{
    #region 인스펙터
    [SerializeField] private int _projectilCount = 3;
    [SerializeField] private float _spiralSpeed = 120f;
    #endregion

    public override UniTask FireAsync(CWeaponFireContext context, CancellationToken ct)
    {
        //유효성 검사
        if (context == null || context.FirePoint == null || context.SpawnProjectile == null)
        {
            return UniTask.CompletedTask;
        }

        ct.ThrowIfCancellationRequested();

        // 검사 (음수 방지)
        int count = Mathf.Max(1, _projectilCount);

        // 카운트에 따른 각도
        float angleRatio = 360f / count;

        // 시간에 따른 회전 누적
        float basicangle = Time.time * _spiralSpeed;

        // _projectilCount가 1발인 경우 
        if (count == 1)
        {
            Quaternion rotation = context.FirePoint.rotation * Quaternion.Euler(0f, basicangle, 0f);

            context.SpawnProjectile(rotation);

            return UniTask.CompletedTask;
        }


        for (int i = 0; i < count; i++)
        {
            ct.ThrowIfCancellationRequested();

            // 최종 각도
            float angle = basicangle + (angleRatio * i);

            // 각도 계산
            Quaternion rotation = context.FirePoint.rotation * Quaternion.Euler(0f, angle, 0f);

            context.SpawnProjectile(rotation);

        }

        Debug.Log("SpiralPattern");

        return UniTask.CompletedTask;
    }

    private void OnValidate()
    {
        // 최소값 보정
        _projectilCount = Mathf.Max(1, _projectilCount);
    }
}