using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/ItemDataSO/PassiveItemEffect/AddModifier", 
    fileName = "PassiveItemAddModifierSO_")]
public class CPassiveItemAddModifierSO : CPassiveItemEffectStrategySO
{

    #region 인스팩터
    [SerializeField] private CMultiflyModifier _modifier;
    #endregion

    public override UniTask OnItemGet()
    {
        CPlayerCharacterController.Instance.ModifierHandler.ApplyModifierWithCheck(_modifier);
        return UniTask.CompletedTask;
    }

    public override UniTask OnItemLost()
    {
        CPlayerCharacterController.Instance.ModifierHandler.RemoveModifier(_modifier);
        return UniTask.CompletedTask;
    }
}
