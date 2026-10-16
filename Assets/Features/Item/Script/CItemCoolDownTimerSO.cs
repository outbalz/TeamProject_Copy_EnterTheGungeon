using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using System;

public class CItemCoolDownTimerSO : CItemCoolDownStrategySO
{
    #region 인스팩터
    [SerializeField] private float _cooldownTime;
    #endregion

    #region 내부변수
    private float _timer;
    #endregion

    public bool OnItemUse(CancellationToken ct)
    {
        if(_timer > 0)
        {
            return false;
        }

        _timer = _cooldownTime;

        CoolingDown(ct).Forget();
        return true;
    }

    private async UniTask CoolingDown(CancellationToken ct)
    {
        while (_timer > 0)
        {
            ct.ThrowIfCancellationRequested();

            _timer -= Time.deltaTime;

            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.DeltaTime, cancellationToken: ct);
        }
    }

}
