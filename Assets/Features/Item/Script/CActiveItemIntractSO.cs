using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CActiveItemIntractSO : CActiveItemEffectStrategySO
{

    public override UniTask OnUse()
    {
        CPlayerInteractionController intraction = CPlayerCharacterController.Instance.InteractionController;

        if(intraction == null)
        {
            return UniTask.CompletedTask;
        }

        if(!intraction.GetInteractObj(out IInteractable interactObj))
        {
            return UniTask.CompletedTask;
        }

        Intract(interactObj).Forget();

        return UniTask.CompletedTask;
    }

    protected abstract UniTask Intract(IInteractable interactObj);
}
