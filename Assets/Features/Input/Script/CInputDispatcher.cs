using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class CInputActionData
{
    [SerializeField] private InputActionReference _reference;
    [SerializeField] private string _actionName;
    
    public InputActionReference Reference => _reference;
    public string ActionName => _actionName;
    public event Action OnPerformedAction;

    public void OnPerformed(InputAction.CallbackContext context)
    {
        OnPerformedAction?.Invoke();
    }

}

[Serializable]
public class CInputVecterActionData
{
    [SerializeField] private InputActionReference _reference;
    [SerializeField] private string _actionName;

    public InputActionReference Reference => _reference;
    public string ActionName => _actionName;
    public event Action<Vector2> OnPerformedAction;

    public void OnPerformed(InputAction.CallbackContext context)
    {
        Vector2 v = context.ReadValue<Vector2>();
        OnPerformedAction?.Invoke(v);
    }

    public void OnCanceled(InputAction.CallbackContext context)
    {
        OnPerformedAction?.Invoke(Vector2.zero);
    }

}

[Serializable]
public class CInputAxisActionData
{
    [SerializeField] private InputActionReference _reference;
    [SerializeField] private string _actionName;

    public InputActionReference Reference => _reference;
    public string ActionName => _actionName;
    public event Action<float> OnPerformedAction;

    public void OnPerformed(InputAction.CallbackContext context)
    {
        float f = context.ReadValue<float>();
        OnPerformedAction?.Invoke(f);
    }

    public void OnCanceled(InputAction.CallbackContext context)
    {
        OnPerformedAction?.Invoke(0f);
    }

}

public class CInputDispatcher : CSingleton<CInputDispatcher>
{
    #region 인스팩터
    [SerializeField] private List<CInputActionData> _inputActions;
    [SerializeField] private List<CInputVecterActionData> _inputVecterActions;
    [SerializeField] private List<CInputAxisActionData> _inputAxisActions;
    #endregion

    #region 내부변수
    private bool _isReady = false;
    #endregion

    #region 프로퍼티
    public readonly Dictionary<string, CInputActionData> InputActionMap = new Dictionary<string, CInputActionData>();
    public readonly Dictionary<string, CInputVecterActionData> InputVecterActionMap = new Dictionary<string, CInputVecterActionData>();
    public readonly Dictionary<string, CInputAxisActionData> InputAxisActionMap = new Dictionary<string, CInputAxisActionData>();
    #endregion

    protected override void Awake()
    {
        base.Awake();

        TryBindAll();
    }

    private void OnEnable()
    {
        TryBindAll();
        EnableActions(true);
        BakeInputMap();
    }

    private void OnDisable()
    {
        EnableActions(false);
        InputActionMap.Clear();
        InputAxisActionMap.Clear();
        InputVecterActionMap.Clear();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        InputActionMap.Clear();
        InputAxisActionMap.Clear();
        InputVecterActionMap.Clear();

        UnBindAll();
    }

    private void TryBind(CInputActionData data)
    {
        if(data.Reference == null)
        {
            return;
        }

        data.Reference.action.performed += data.OnPerformed;
    }

    private void TryBind(CInputVecterActionData data)
    {
        if (data.Reference == null)
        {
            Debug.LogWarning("액션 레퍼런스 누락");
            return;
        }

        data.Reference.action.performed += data.OnPerformed;
        data.Reference.action.canceled += data.OnCanceled;
    }

    private void TryBind(CInputAxisActionData data)
    {
        if (data.Reference == null)
        {
            Debug.LogWarning("액션 레퍼런스 누락");
            return;
        }

        data.Reference.action.performed += data.OnPerformed;
        data.Reference.action.canceled += data.OnCanceled;
    }

    private void UnBind(CInputActionData data)
    {
        if(data.Reference == null)
        {
            Debug.LogWarning("액션 레퍼런스 누락");
            return;
        }

        data.Reference.action.performed -= data.OnPerformed;
    }
    
    private void UnBind(CInputVecterActionData data)
    {
        if (data.Reference == null)
        {
            Debug.LogWarning("액션 레퍼런스 누락");
            return;
        }

        data.Reference.action.performed -= data.OnPerformed;
        data.Reference.action.canceled -= data.OnCanceled;
    }
    
    private void UnBind(CInputAxisActionData data)
    {
        if (data.Reference == null)
        {
            Debug.LogWarning("액션 레퍼런스 누락");
            return;
        }

        data.Reference.action.performed -= data.OnPerformed;
        data.Reference.action.canceled -= data.OnCanceled;
    }

    private void TryBindAll()
    {
        if(_isReady)
        {
            return;
        }

        for (int i = 0; i < _inputActions.Count; i++)
        {
            if(_inputActions[i] == null)
            {
                continue;
            }

            TryBind(_inputActions[i]);
        }

        for (int i = 0; i < _inputVecterActions.Count; i++)
        {
            if(_inputVecterActions[i] == null)
            {
                continue;
            }

            TryBind(_inputVecterActions[i]);
        }

        for (int i = 0; i < _inputAxisActions.Count; i++)
        {
            if(_inputAxisActions[i] == null)
            {
                continue;
            }

            TryBind(_inputAxisActions[i]);
        }

        _isReady = true;
    }

    private void UnBindAll()
    {
        if(!_isReady)
        {
            return;
        }

        for (int i = 0; i < _inputActions.Count; i++)
        {
            if(_inputActions[i] == null)
            {
                continue;
            }

            UnBind(_inputActions[i]);
        }

        for (int i = 0; i < _inputVecterActions.Count; i++)
        {
            if(_inputVecterActions[i] == null)
            {
                continue;
            }

            UnBind(_inputVecterActions[i]);
        }

        for (int i = 0; i < _inputAxisActions.Count; i++)
        {
            if(_inputAxisActions[i] == null)
            {
                continue;
            }

            UnBind(_inputAxisActions[i]);
        }

        _isReady = false;
    }

    private void EnableActions(bool enable)
    {

        if (!_isReady)
        {
            return;
        }

        if (enable)
        {
            for (int i = 0; i < _inputActions.Count; i++)
            {
                if (_inputActions[i] == null)
                {
                    continue;
                }

                _inputActions[i].Reference.action.Enable();
            }

            for (int i = 0; i < _inputVecterActions.Count; i++)
            {
                if (_inputVecterActions[i] == null)
                {
                    continue;
                }

                _inputVecterActions[i].Reference.action.Enable();
            }

            for (int i = 0; i < _inputAxisActions.Count; i++)
            {
                if (_inputAxisActions[i] == null)
                {
                    continue;
                }

                _inputAxisActions[i].Reference.action.Enable();
            }
        }

        else
        {

            for (int i = 0; i < _inputActions.Count; i++)
            {
                if (_inputActions[i] == null)
                {
                    continue;
                }

                _inputActions[i].Reference.action.Disable();
            }

            for (int i = 0; i < _inputVecterActions.Count; i++)
            {
                if (_inputVecterActions[i] == null)
                {
                    continue;
                }

                _inputVecterActions[i].Reference.action.Disable();
            }

            for (int i = 0; i < _inputAxisActions.Count; i++)
            {
                if (_inputAxisActions[i] == null)
                {
                    continue;
                }

                _inputAxisActions[i].Reference.action.Disable();
            }
        }
    }

    private void BakeInputMap()
    {

        for (int i = 0; i < _inputActions.Count; i++)
        {

            if (_inputActions[i] == null)
            {
                continue;
            }

            CInputActionData action = _inputActions[i];

            if (InputActionMap.ContainsKey(action.ActionName))
            {
                continue;
            }

            InputActionMap.Add(action.ActionName, action);
        }

        for (int i = 0; i < _inputVecterActions.Count; i++)
        {
            if (_inputVecterActions[i] == null)
            {
                continue;
            }

            CInputVecterActionData action = _inputVecterActions[i];

            if (InputVecterActionMap.ContainsKey(action.ActionName))
            {
                continue;
            }

            InputVecterActionMap.Add(action.ActionName, action);
        }

        for (int i = 0; i < _inputAxisActions.Count; i++)
        {
            if (_inputAxisActions[i] == null)
            {
                continue;
            }

            CInputAxisActionData action = _inputAxisActions[i];

            if (InputAxisActionMap.ContainsKey(action.ActionName))
            {
                continue;
            }

            InputAxisActionMap.Add(action.ActionName, action);
        }
    }
}
