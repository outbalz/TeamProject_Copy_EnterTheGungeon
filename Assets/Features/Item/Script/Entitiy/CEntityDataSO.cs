using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/EntityDataSO/EntityData", fileName = "EntityDataSO_")]
public class CEntityDataSO : CGameDataSO
{
	#region 인스팩터
	[SerializeField] private Sprite _sprite;
	[SerializeField] private bool _isInteractable;
	[SerializeField] private CEntityStrategySO _strategy;
	#endregion

	#region 프로퍼티
	public Sprite Sprite => _sprite;
	public CEntityStrategySO Strategy => _strategy;
	#endregion

	public bool IsInteractable(CEntityController entity)
	{ 
		{
			if(_strategy != null)
			{
				return (_isInteractable && _strategy.InteractCheck(entity));
			}

			return _isInteractable;
		} 
	}

	public void InitializeEntitiy(CEntityController entity)
	{
		entity.SpriteRenderer.sprite = _sprite;
		_strategy.InitializeEntitiy(entity);
	}
}
