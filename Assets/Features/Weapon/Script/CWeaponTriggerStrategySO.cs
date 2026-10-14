using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CWeaponTriggerStrategySO : CGameDataSO
{
    #region 인스팩터

    #endregion

    #region 프로퍼티

    #endregion

    public abstract UniTask TriggerWeapon();
}
