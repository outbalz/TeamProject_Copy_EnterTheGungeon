using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    #region 인스팩터
    [SerializeField] private T _prefab;
	[SerializeField] private int _prewarmCount;
    [SerializeField] private Transform _poolRoot;
    #endregion

    #region 내부변수
	private readonly Queue<T> _pool = new Queue<T>();
	private readonly List<T> _liveList = new List<T>();
    #endregion


    private void CreatePoolRoot()
    {
        if (_poolRoot != null)
        {
            return;
        }

        GameObject root = new GameObject("EnemyPool_Root");

        _poolRoot = root.transform;
    }

    private void Prewarm()
    {
        CreatePoolRoot();

        for (int i = 0; i < _prewarmCount; i++)
        {
            T obj = Instantiate(_prefab, _poolRoot);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public T GetFromPool()
    {
        T obj;

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

    public void ReturnToPool(T obj)
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

    public List<T> GetAliveList()
    {
        return _liveList;
    }
}
