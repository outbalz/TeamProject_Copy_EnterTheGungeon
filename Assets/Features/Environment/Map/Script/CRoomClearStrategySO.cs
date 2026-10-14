using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CRoomClearStrategySO : CGameDataSO
{

    public abstract UniTask OnRoomClear();
}
