using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActiveItemLockpickSO : CActiveItemIntractSO
{
    #region 인스팩터
    [SerializeField] private float _lockpitChance = 50f;
    #endregion


    protected override UniTask Intract(IInteractable interactObj)
    {
        if(interactObj == null)
        {
            return UniTask.CompletedTask;
        }

        if(!(interactObj is CInteractableChestController chest))
        {
            return UniTask.CompletedTask;
        }

        if (!chest.IsInteractable)
        {
            return UniTask.CompletedTask;
        }

        float ranNum = Random.Range(0f, 100f);

        float chance = _lockpitChance;

        if (CPlayerCharacterController.Instance.ModifierHandler != null)
        {
            chance = CPlayerCharacterController.Instance.ModifierHandler.GetModifiedValue("LuckModifier", _lockpitChance);
        }

        if(chance >= ranNum)
        {
            //TODO : get chest item
        }

        else
        {
            //TODO : lock Chest
            chest.IsInteractable = false;
        }

        return UniTask.CompletedTask;
    }
}
