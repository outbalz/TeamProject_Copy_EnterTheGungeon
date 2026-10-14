using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapRoomDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] private CRoomEnterStrategySO _roomEnter;
    [SerializeField] private CRoomClearStrategySO _roomClear;
    #endregion

    #region 프로퍼티
    public CRoomEnterStrategySO RoomEnter => _roomEnter;
    public CRoomClearStrategySO RoomClear => _roomClear;
    #endregion


}
