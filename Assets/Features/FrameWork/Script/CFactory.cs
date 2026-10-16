using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CFactory<TFactoy, TReuslt> : CSingleton<TFactoy> where TFactoy : MonoBehaviour where TReuslt : MonoBehaviour
{
	#region 인스팩터
	[SerializeField] protected TReuslt _prefab;
	#endregion

	protected override void Awake()
	{
		base.Awake();
		
		PrefabCheck();
	}

	protected virtual void PrefabCheck()
	{
        if (_prefab == null)
        {
            Debug.LogWarning("팩토리 프리펩 누락");
            enabled = false;
            return;
        }
    }

	public virtual TReuslt Creat()
	{
		return Instantiate(_prefab);
	}

	public virtual TReuslt Creat(Transform tr)
	{
        return Instantiate(_prefab, tr);
    }
}
