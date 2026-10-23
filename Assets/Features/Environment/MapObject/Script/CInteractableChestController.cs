using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class CInteractableChestController : CInteractalbeController
{

    
    private string _playerTag = "Player";
    private string _actionInteract = "Interact";

    [SerializeField]
    private Vector2 _veloticy;

    [SerializeField]
    CEntityDatabaseSO _itemDataSO;

    private void OnEnable()
    { 
        _veloticy = new Vector2(Random.Range(0f,2f), Random.Range(0f, 2f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _playerTag)
        {
            
            CInputDispatcher.Instance.InputActionMap[_actionInteract].OnPerformedAction += Interact;

            IsInteractable = true;
        }
    }
     
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == _playerTag)
        {
             
            CInputDispatcher.Instance.InputActionMap[_actionInteract].OnPerformedAction -= Interact;

            IsInteractable = false;
        }
    }
     
    public override void Interact()
    { 
        if (!IsInteractable)
        {
            return;
        }

        if(_itemDataSO  == null)
        {
            return;
        }

        
        int randomNum = Random.Range(0, _itemDataSO.Count);
        CEntityFactory.Instance.CreatAndTrow(_veloticy, _itemDataSO[randomNum], transform.position);
         
    }
}
