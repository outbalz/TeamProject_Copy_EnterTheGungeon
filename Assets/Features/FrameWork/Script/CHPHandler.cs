using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CHPHandler : MonoBehaviour
{
    #region 인스팩터

    #endregion

    #region 내부변수
    protected float _hp;
    #endregion

    #region 프로퍼티
    public float HP { get { return _hp; } set { _hp = value; } }
    public event Action OnDeath;
    #endregion

    public abstract UniTask OnHit();

}
