using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CPassiveItemEffectStrategySO : CGameDataSO
{
    public abstract UniTask OnItemGet();
    public abstract UniTask OnItemLost();

}
