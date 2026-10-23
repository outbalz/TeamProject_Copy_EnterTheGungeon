using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEntityExplodeSO : CEntityDestroyStrategySO
{
    #region 인스팩터
    [SerializeField] private float _explosionDamage;
    [SerializeField] private float _explosionRange;
    //
    #endregion

    public override void OnEnitityDestoryed()
    {
        throw new System.NotImplementedException();
    }
}
