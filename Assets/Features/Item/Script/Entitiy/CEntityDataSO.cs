using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEntityDataSO : CGameDataSO
{
	#region 인스팩터
	[SerializeField] private Sprite _sprite;
	[SerializeField] private bool _isInteractable;
	[SerializeField] private CEntityStrategySO _strategy;
	#endregion

	#region 프로퍼티
	public Sprite Sprite => _sprite;
	public bool IsInteractable => _isInteractable;
	#endregion

	public void InitializeEntitiy(CEntityController entity)
	{
		_strategy.InitializeEntitiy(entity);
	}
}
