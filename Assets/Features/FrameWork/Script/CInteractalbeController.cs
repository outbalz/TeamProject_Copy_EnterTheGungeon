using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CInteractalbeController : MonoBehaviour, IInteractable
{


    #region 내부변수 
    private bool _isInteractable;
    #endregion

    #region 프로퍼티
    public bool IsInteractable { get { return _isInteractable; } set { _isInteractable = value; } }
    #endregion
     
    public abstract void Interact();

    
}
