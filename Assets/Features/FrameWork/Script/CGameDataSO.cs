using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CGameDataSO : ScriptableObject
{
	#region 인스팩터
	[SerializeField] protected string _id;
	[SerializeField] protected string _name;
	#endregion

	#region 프로퍼티
	public string ID => _id;
	public string Name => _name;
    #endregion

    protected virtual void OnValidate()
    {
		if (string.IsNullOrWhiteSpace(_id))
		{
			Debug.LogWarning("_id가 적합하지 않습니다");
		}

		if (string.IsNullOrWhiteSpace(_name))
		{
			Debug.LogWarning("_name이 적합하지 않습니다");
		}
    }
}
