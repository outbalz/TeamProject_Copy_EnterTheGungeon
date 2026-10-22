using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CPassiveItemOnHitEffectSO : CPassiveItemEffectStrategySO
{
    public override UniTask OnItemGet()
    {
        CPlayerCharacterController.Instance.HPhandler.OnHitEvent += OnHitEfect;
        return UniTask.CompletedTask;
    }

    public override UniTask OnItemLost()
    {
        CPlayerCharacterController.Instance.HPhandler.OnHitEvent -= OnHitEfect;
        return UniTask.CompletedTask;
    }

    protected abstract void OnHitEfect();
}
