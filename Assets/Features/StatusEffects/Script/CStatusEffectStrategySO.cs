using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CStatusEffectStrategySO : CGameDataSO
{
    public abstract void OnApply(CStatusEffectHandler handler);

    public abstract void OnRemoved(CStatusEffectHandler handler);
}
