using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CHPHandler : MonoBehaviour
{
    #region 인스팩터
    [SerializeField] protected float _maxHp;
    #endregion

    #region 내부변수
    protected float _hp;
    #endregion

    #region 프로퍼티
    public float HP { get { return _hp; } set { _hp = Mathf.Clamp(value, 0, _maxHp); } }
    public float MaxHP { get { return _maxHp; } set { _maxHp = value; } }
    public event Action OnHitEvent;
    public event Action OnDeath;
    #endregion

    public abstract UniTask OnHit(float damage);

    public void OnDeathInvoke()
    {
        OnDeath?.Invoke();
    }

    public void OnHitInvoke()
    {
        OnHitEvent?.Invoke();
    }

}
