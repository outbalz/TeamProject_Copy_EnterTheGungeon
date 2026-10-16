using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CItemController : MonoBehaviour
{
    #region 인스팩터
    [SerializeField] private string _itemUseInputName = "UseItem";
    #endregion

    #region 내부변수
    private List<CActiveItemDataSO> _activeItems = new List<CActiveItemDataSO>();
    private List<CPassiveItemDataSO> _passiveItems = new List<CPassiveItemDataSO>();
    private Dictionary<CActiveItemDataSO, float> _cooldownMap = new Dictionary<CActiveItemDataSO, float>();
    private int _activeItemIndex = 0;

    private bool _isBinded = false;
    #endregion

    #region 프로퍼티

    #endregion

    private void OnEnable()
    {
        TryBind();
    }

    private void OnDisable()
    {
        UnBind();
    }

    private void OnDestroy()
    {
        UnBind();
    }

    private void TryBind()
    {
        if (_isBinded)
        {
            return;
        }

        Dictionary<string, CInputActionData> InputActionMap = CInputDispatcher.Instance.InputActionMap;

        if (InputActionMap == null)
        {
            Debug.LogWarning("인풋엑션맵 누락");
            return;
        }

        if (!InputActionMap.ContainsKey(_itemUseInputName))
        {
            Debug.LogWarning("인풋엑션 누락");
            return;
        }

        InputActionMap[_itemUseInputName].OnPerformedAction += OnItemUse;
        _isBinded = true;
    }

    private void UnBind()
    {
        if (!_isBinded)
        {
            return;
        }

        Dictionary<string, CInputActionData> InputActionMap = CInputDispatcher.Instance.InputActionMap;

        if (InputActionMap == null)
        {
            Debug.LogWarning("인풋엑션맵 누락");
            return;
        }

        if (!InputActionMap.ContainsKey(_itemUseInputName))
        {
            Debug.LogWarning("인풋엑션 누락");
            return;
        }

        InputActionMap[_itemUseInputName].OnPerformedAction -= OnItemUse;
        _isBinded = false;
    }

    private void OnItemUse()
    {
        if (_activeItems[_activeItemIndex] == null)
        {
            return;
        }

        _activeItems[_activeItemIndex].OnItemUes();
    }

}
