using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjectPoolFactory<T> : CFactory<T> where T : MonoBehaviour
{
    #region MyRegion
    [SerializeField] private CObjectPool<T> _pool;
    #endregion

    public override T Creat()
    {
        return _pool.GetFromPool();
    }

    public override T Creat(Transform tr)
    {
        T obj = _pool.GetFromPool();

        if (obj == null)
        {
            return null;
        }

        obj.transform.SetParent(tr);

        return obj;
    }
}
