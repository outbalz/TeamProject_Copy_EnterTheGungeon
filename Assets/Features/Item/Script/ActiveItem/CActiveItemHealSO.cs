using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActiveItemHealSO : CActiveItemEffectStrategySO
{
	#region 인스팩터
	[SerializeField] private float _healAmount;
    #endregion

    public override UniTask OnUse()
    {
        CPlayerCharacterController.Instance.HPhandler.HP += _healAmount;
        //TODO 체대 체럭 이상 치료 방지하기

        return UniTask.CompletedTask;
    }
}
