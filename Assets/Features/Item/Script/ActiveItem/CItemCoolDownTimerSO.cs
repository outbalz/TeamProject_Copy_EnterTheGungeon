using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class CItemCoolDownTimerSO : CItemCoolDownStrategySO
{
    #region 인스팩터
    [SerializeField] private float _cooldownTime;
    #endregion


    public override bool OnItemUse(CActiveItem item)
    {
        if(item.Cooldown > 0)
        {
            return false;
        }

        item.Cooldown = _cooldownTime;
        item.CTS = new CancellationTokenSource();
        CoolingDown(item).Forget();
        return true;
    }

    private async UniTask CoolingDown(CActiveItem item)
    {
        while (item.Cooldown > 0)
        {
            item.CTS.Token.ThrowIfCancellationRequested();

            item.Cooldown -= Time.deltaTime;

            await UniTask.NextFrame(PlayerLoopTiming.Update, item.CTS.Token);
        }
    }

}
