using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CRoomEnterStrategySO : CGameDataSO
{

    public abstract UniTask OnRoomEnter();
}
