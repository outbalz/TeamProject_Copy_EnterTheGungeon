using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CFactory<T> : CSingleton<CFactory<T>> where T : MonoBehaviour
{
	#region 인스팩터
	[SerializeField] protected T _prefab;
	#endregion

	protected override void Awake()
	{
		base.Awake();

		if( _prefab == null)
		{
			Debug.LogWarning("팩토리 프리펩 누락");
			enabled = false;
			return;
		}
	}

	public virtual T Creat()
	{
		return Instantiate(_prefab);
	}

	public virtual T Creat(Transform tr)
	{
		return Instantiate(_prefab, tr);
	}
}
