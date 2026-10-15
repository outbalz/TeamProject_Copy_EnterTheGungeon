using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerInteractionController : MonoBehaviour
{
	#region 내부 변수
	private IInteractable _interactObj;
	#endregion

	public bool GetInteractObj(out IInteractable interactObj) 
	{ 
		interactObj = _interactObj;

		if(_interactObj == null)
		{
			return false;
		}

		return true;
	}
}
