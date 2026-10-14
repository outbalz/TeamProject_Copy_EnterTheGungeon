using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CWeaponDataSO : CGameDataSO
{
    #region 내부 변수
    [SerializeField] CWeaponTriggerStrategySO _trigger;
    #endregion

    #region 프로퍼티
    public CWeaponTriggerStrategySO Trigger => _trigger;
    #endregion

}
