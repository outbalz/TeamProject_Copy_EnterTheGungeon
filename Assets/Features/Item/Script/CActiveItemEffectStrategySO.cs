using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CActiveItemEffectStrategySO : CGameDataSO
{
    public abstract UniTask OnUse();
}
