using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CGameDataSO : ScriptableObject
{
	#region 인스팩터
	[SerializeField] protected string _id = "id";
	[SerializeField] protected string _name = "name";
	#endregion

	#region 프로퍼티
	public string ID => _id;
	public string Name => _name;
    #endregion

    protected virtual void OnValidate()
    {
		if (string.IsNullOrWhiteSpace(_id))
		{
			Debug.LogWarning($"{this}/ {_id} :_id가 적합하지 않습니다");
		}

		if (string.IsNullOrWhiteSpace(_name))
		{
			Debug.LogWarning($"{this}/ {_name} :_name이 적합하지 않습니다");
		}
    }
}
