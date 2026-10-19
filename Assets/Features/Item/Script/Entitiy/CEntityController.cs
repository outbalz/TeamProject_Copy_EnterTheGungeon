using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEntityController : CInteractalbeController
{
	#region 인스팩터
	[SerializeField] private Rigidbody2D _rb;
    #endregion

    #region 내부변수
    private CEntityDataSO _dataSO;
    #endregion

    #region 프로퍼티
    public Rigidbody2D Rb => _rb;
    public event Action OnInteractEvent;
    public event Action<Collider2D> OnTriggerEnterEvent;
    #endregion

    public void InitializeEntitiy(CEntityDataSO data)
    {
        _dataSO = data;
        data.InitializeEntitiy(this);
    }

    public override void Interact()
    {
        OnInteractEvent?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnterEvent?.Invoke(collision);
    }

    private void OnDestroy()
    {
        OnInteractEvent = null;
        OnTriggerEnterEvent = null;
    }
}
