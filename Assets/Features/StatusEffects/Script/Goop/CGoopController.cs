using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoopController : MonoBehaviour, IIgniteable, IElectrifiable, IFrozeable
{
    #region 내부변수
    private CGoopDataSO _dataSO;
    #endregion

    public void Ignite()
    {
        if (!_dataSO.CanIgnited)
        {
            return;
        }


    }

    public void Electrifie()
    {
        if (!_dataSO.CanElectrified)
        {
            return;
        }
    }

    public void Froze()
    {
        if (!_dataSO.CanFrozen)
        {
            return;
        }
    }
}
