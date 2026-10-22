using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/GameDataSO/ItemDataSO/PassiveItemDataSO", fileName = "PassiveItemDataSO_")]
public class CPassiveItemDataSO : CGameDataSO
{
	#region 인스팩터
	[SerializeField] protected CPassiveItemEffectStrategySO[] _effect;
	#endregion

	public void OnItemGet()
	{
		for (int i = 0; i < _effect.Length; i++)
		{
			_effect[i].OnItemGet().Forget();
		}
	}


    public void OnItemLost()
    {
        for (int i = 0; i < _effect.Length; i++)
        {
            _effect[i].OnItemLost().Forget();
        }
    }
}
