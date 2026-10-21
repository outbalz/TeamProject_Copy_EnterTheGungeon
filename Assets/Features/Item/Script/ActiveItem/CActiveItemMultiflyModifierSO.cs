using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "SO/GameDataSO/ItemDataSO/ActiveItemEffect/Modifier", fileName = "ActiveItemModifierSO_")]
public class CActiveItemMultiflyModifierSO : CActiveItemEffectStrategySO
{
    #region 인스팩터
    [SerializeField] private CMultiflyModifier _modifier;
    #endregion

    public override UniTask OnUse()
    {
        CModifier modifier = new CMultiflyModifier(_modifier.ModifierName, _modifier.ModifierValue);
        CPlayerCharacterController.Instance.ModifierHandler.ApplyModifier(modifier);
        //TODO 모디파이어 제거 조건 추가하기

        return UniTask.CompletedTask;
    }
}
