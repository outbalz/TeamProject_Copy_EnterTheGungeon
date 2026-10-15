using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCharacterController : CSingleton<CPlayerCharacterController>
{
	#region 인스팩터
	[SerializeField] private CPlayerInteractionController _interactionController;
	[SerializeField] private CPlayerHPHandler _hpHandler;
	[SerializeField] private CModifierHandler _modifierHandler;
	#endregion

	#region 프로퍼티
	public CPlayerInteractionController InteractionController => _interactionController;
	public CPlayerHPHandler HPhandler => _hpHandler;
	public CModifierHandler ModifierHandler => _modifierHandler;
	#endregion
}
