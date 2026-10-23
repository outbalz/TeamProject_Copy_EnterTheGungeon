using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CInteractableChestController : CInteractalbeController
{

    InputAction inputActions; 
    private string _playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _playerTag)
        {
            IsInteractable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == _playerTag)
        {
            IsInteractable = false;
        }
    }
     
    public override void Interact()
    {
        //CInputDispatcher.Instance.InputActionMap
        //Do Something

        //플레이어가 가까이 오면 가능 상태
        //
        if (IsInteractable)
        {

        }

        
    }
}
