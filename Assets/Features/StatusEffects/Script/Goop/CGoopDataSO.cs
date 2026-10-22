using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoopDataSO : CGameDataSO
{
	#region 인스팩터
	[SerializeField] private bool _canIgnited;
	[SerializeField] private bool _canElectrified;
	[SerializeField] private bool _canFrozen;

	//스프아이트 에셋 찾기 전까지 임시용
	[SerializeField] private Color _color;
	#endregion

	#region 프로퍼티
	public bool CanIgnited => _canIgnited;
	public bool CanElectrified => _canElectrified;
	public bool CanFrozen => _canFrozen;
	public Color GoopColor => _color;
	#endregion

	public virtual void OnCreat()
	{
		//Do Nothing
	}
}

