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
    
    //사용 가능 횟수, -1경우 무제한
    public int UseAmount;

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
        UseAmount = dataSO.UseAmount;
    }
}

public class CItemController : CSingleton<CItemController>
{
    #region 인스팩터
    [SerializeField] private string _itemUseInputName = "UseItem";

    [Header("시작 아이템")]
    [SerializeField] private CActiveItemDataSO _startActiveItem;
    [SerializeField] private List<CPassiveItemDataSO> _startPassiveItem;
    #endregion

    #region 내부변수
    private List<CActiveItem> _activeItems = new List<CActiveItem>();
    private List<CPassiveItemDataSO> _passiveItems = new List<CPassiveItemDataSO>();
    private int _activeItemIndex = 0;
    private int _itemSlotSize = 1;

    private bool _isBinded = false;
    #endregion

    #region 프로퍼티

    #endregion

    private void Start()
    {
        AddActiveItem(_startActiveItem);

        for (int i = 0; i < _startPassiveItem.Count; i++)
        {
            AddPassiveItem(_startPassiveItem[i]);
        }
    }

    private void OnEnable()
    {
        TryBind();
    }

    private void OnDisable()
    {
        UnBind();
    }

    protected override void OnDestroy()
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

        base.OnDestroy();
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

        //사용가능한 상태인지 확인하고 사용, 아니라면 리턴
        if (!_activeItems[_activeItemIndex].DataSO.OnItemUes(_activeItems[_activeItemIndex]))
        {
            return;
        }

        if(_activeItems[_activeItemIndex].UseAmount != -1)
        {
            _activeItems[_activeItemIndex].UseAmount--;
        }

        if (_activeItems[_activeItemIndex].UseAmount == 0)
        {
            _activeItems.RemoveAt(_activeItemIndex);
            _activeItemIndex = Mathf.Max(_activeItemIndex - 1, 0);
        }

    }

    public void AddActiveItem(CActiveItemDataSO item)
    {
        int slotSize = _itemSlotSize;

        if (CPlayerCharacterController.Instance.ModifierHandler != null)
        {
            slotSize = Mathf.RoundToInt(CPlayerCharacterController.Instance.ModifierHandler.GetModifiedValue("ItemSlot", _itemSlotSize));
        }

        if (_activeItems.Count < _itemSlotSize)
        {
            _activeItems.Add(new CActiveItem(item, 0));
        }

        else
        {
            DropItem(_activeItems[_activeItemIndex].DataSO);
            _activeItems[_activeItemIndex] = new CActiveItem(item, 0);
        }
    }

    public void AddPassiveItem(CPassiveItemDataSO item)
    {
        _passiveItems.Add(item);
    }

    public void DropItem(CActiveItemDataSO item)
    {
        CEntityDataSO entityData = item.ItemEntitySO;
        CEntityController itemEntity = CEntityFactory.Instance.Creat(entityData);
        itemEntity.transform.position = CPlayerCharacterController.Instance.transform.position;
    }

    public void DropItem(CPassiveItemDataSO item)
    {
        CEntityDataSO entityData = item.ItemEntitySO;
        CEntityController itemEntity = CEntityFactory.Instance.Creat(entityData);
        itemEntity.transform.position = CPlayerCharacterController.Instance.transform.position;
    }
}
