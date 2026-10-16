using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemCoolDownDamageDealtSO : CItemCoolDownStrategySO
{
    #region 인스팩터
    [SerializeField] private float _requirDamage;
    #endregion

    #region 내부변수
    private float _damageDealt;
    #endregion


}
