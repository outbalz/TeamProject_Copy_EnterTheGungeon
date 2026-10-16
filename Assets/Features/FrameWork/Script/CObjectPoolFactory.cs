using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjectPoolFactory<TFactoy, TReuslt> : CFactory<TFactoy, TReuslt> where TFactoy : MonoBehaviour where TReuslt : MonoBehaviour
{
    #region MyRegion
    [SerializeField] private CObjectPool<TReuslt> _pool;
    #endregion

    public override TReuslt Creat()
    {
        return _pool.GetFromPool();
    }

    public override TReuslt Creat(Transform tr)
    {
        TReuslt obj = _pool.GetFromPool();

        if (obj == null)
        {
            return null;
        }

        obj.transform.SetParent(tr);

        return obj;
    }
}
