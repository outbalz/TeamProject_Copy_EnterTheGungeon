using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/ItemDataSO/PassiveItemEffect/OnHitEnrage",
    fileName = "PassiveItemEnrageSO_")]
public class CPassiveItemEnrageSO : CPassiveItemOnHitEffectSO
{
    #region 인스팩터
    [SerializeField] private float _enrageTime;
    [SerializeField] private CMultiflyModifier _modifier;
    #endregion

    protected override void OnHitEfect()
    {
        Enrage().Forget();
    }

    private async UniTask Enrage()
    {
        //apply enrage effect
        CPlayerCharacterController.Instance.ModifierHandler.ApplyModifierWithCheck(_modifier);

        await UniTask.Delay(TimeSpan.FromSeconds(_enrageTime));
        
        //remove enrage effect
        CPlayerCharacterController.Instance.ModifierHandler.RemoveModifier(_modifier);
    }
}
