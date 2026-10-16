using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class CActiveItem
{
    public CActiveItemDataSO DataSO;
    public float Cooldown;
    public CancellationTokenSource CTS;

    public void Cancel()
    {
        if(CTS == null)
        {
            return;
        }

        CTS.Cancel();
        CTS.Dispose();
        CTS = null;
    }

    public CActiveItem(CActiveItemDataSO dataSO, float coolDown)
    {
        DataSO = dataSO;
        Cooldown = coolDown;
    }
}

public class CItemController : MonoBehaviour
{
    #region 인스팩터
    [SerializeField] private string _itemUseInputName = "UseItem";
    #endregion

    #region 내부변수
    private List<CActiveItem> _activeItems = new List<CActiveItem>();
    private List<CPassiveItemDataSO> _passiveItems = new List<CPassiveItemDataSO>();
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
        for (int i = 0; i < _activeItems.Count; i++)
        {
            if(_activeItems[i] == null)
            {
                continue;
            }

            _activeItems[i].Cancel();
        }

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
        if(_activeItems.Count == 0)
        {
            return;
        }

        if (_activeItems.Count <= _activeItemIndex || 0 > _activeItemIndex)
        {
            Debug.LogWarning("인덱스 오류");
            return;
        }

        if (_activeItems[_activeItemIndex] == null)
        {
            Debug.LogWarning("아이템 누락");
            return;
        }

        _activeItems[_activeItemIndex].DataSO.OnItemUes(_activeItems[_activeItemIndex]);
    }


    public void AddActiveItem(CActiveItemDataSO item)
    {
        _activeItems.Add(new CActiveItem(item, 0));
    }

}
