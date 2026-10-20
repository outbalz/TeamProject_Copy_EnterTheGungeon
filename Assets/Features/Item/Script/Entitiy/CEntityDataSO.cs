using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEntityDataSO : CGameDataSO
{
	#region 인스팩터
	[SerializeField] private bool _isInteractable;
	[SerializeField] private CEntityStrategySO _strategy;
    #endregion

	public void InitializeEntitiy(CEntityController entity)
	{
		_strategy.InitializeEntitiy(entity);
	}
}
