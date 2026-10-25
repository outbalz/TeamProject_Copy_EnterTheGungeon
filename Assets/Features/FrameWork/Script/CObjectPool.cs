using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CObjectPool<TPool, TReuslt> : CSingleton<TPool> where TPool : MonoBehaviour where TReuslt : MonoBehaviour
{
    #region 인스팩터
    [SerializeField] protected TReuslt _prefab;
	[SerializeField] protected int _prewarmCount;
    [SerializeField] protected Transform _poolRoot;
    #endregion

    #region 내부변수
	protected readonly Queue<TReuslt> _pool = new Queue<TReuslt>();
	protected readonly List<TReuslt> _liveList = new List<TReuslt>();
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
            Debug.LogWarning("오브젝트풀 프리펩 누락");
            enabled = false;
            return;
        }
    }

    public void CreatePoolRoot()
    {
        if (_poolRoot != null)
        {
            return;
        }

        GameObject root = new GameObject("EnemyPool_Root");

        _poolRoot = root.transform;
    }

    public void Prewarm()
    {
        CreatePoolRoot();

        for (int i = 0; i < _prewarmCount; i++)
        {
            TReuslt obj = Instantiate(_prefab, _poolRoot);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public TReuslt GetFromPool()
    {
        TReuslt obj;

        if (_pool.Count > 0)
        {
            obj = _pool.Dequeue();
        }

        else
        {
            obj = Instantiate(_prefab);
        }

        _liveList.Add(obj);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(TReuslt obj)
    {
        if (obj == null)
        {
            return;
        }

        if (_liveList.Contains(obj))
        {
            _liveList.Remove(obj);
        }

        obj.transform.SetParent(_poolRoot);
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    public List<TReuslt> GetAliveList()
    {
        return _liveList;
    }
}
