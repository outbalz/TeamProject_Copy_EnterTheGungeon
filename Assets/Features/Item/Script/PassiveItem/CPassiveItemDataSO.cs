using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/GameDataSO/ItemDataSO/PassiveItemDataSO", fileName = "PassiveItemDataSO_")]
public class CPassiveItemDataSO : CGameDataSO
{
	#region 인스팩터
	[SerializeField] protected CPassiveItemEffectStrategySO[] _effect;
    [SerializeField] protected CEntityDataSO _itemEntitySO;
    #endregion

    #region 프로퍼티
    public CEntityDataSO ItemEntitySO => _itemEntitySO;
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
