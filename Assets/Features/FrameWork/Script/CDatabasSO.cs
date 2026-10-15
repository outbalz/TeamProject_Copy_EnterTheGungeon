using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CDatabasSO<T> : ScriptableObject, ISerializationCallbackReceiver where T : CGameDataSO
{
	#region 인스팩터
	[SerializeField] protected List<T> _datas = new List<T>();
    #endregion


    #region private var
    [NonSerialized] private Dictionary<string, T> _dataMap;
    [NonSerialized] private bool _isDrty = true;
    #endregion

    #region property
    public IReadOnlyList<T> Datas => _datas;
    public int Count => _datas.Count;
    #endregion

    #region indexer
    public T this[int index] => _datas[index];
    #endregion

    public bool TryGet(string ID, out T data)
    {
        EnsureDataMap();

        return _dataMap.TryGetValue(ID, out data);
    }

    private void EnsureDataMap()
    {
        if (!_isDrty && _dataMap != null)
        {
            return;
        }

        _dataMap ??= new Dictionary<string, T>();

        _dataMap.Clear();

        for (int i = 0; i < _datas.Count; i++)
        {
            T data = _datas[i];

            if (data == null || string.IsNullOrWhiteSpace(data.ID))
            {
                continue;
            }

            _dataMap.TryAdd(data.ID, data);
        }

        _isDrty = false;
    }


    protected virtual void OnValidate()
    {
        HashSet<string> ids = new HashSet<string>();

        for (int i = 0; i < _datas.Count; i++)
        {
            T data = _datas[i];

            if (data == null)
            {
                continue;
            }

            string ID = data.ID;

            if (string.IsNullOrWhiteSpace(ID))
            {
                Debug.LogWarning($"{name} / {i}의 아이디가 적합하지 않습니다.");
                continue;
            }

            if (!ids.Add(ID))
            {
                Debug.LogWarning($"{name} : 중복 아이디 {ID}");
            }
        }

        _isDrty = true;
    }

    public void OnBeforeSerialize()
    {
        //Do nothing
    }

    public void OnAfterDeserialize()
    {
        _isDrty = true;
    }
}
