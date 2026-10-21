using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProjectileEffectHandler 
{
    private CProjectileEffectSO _effectSO;

    public CProjectileEffectHandler(CProjectileEffectSO effectSO)
    {
        _effectSO = effectSO;
    }

    public void HitEffect(GameObject target)
    {
        if (_effectSO == null)
        {
            return;
        }
        
        switch(_effectSO.PropertyType)
        {
            case PropertyType.None:

                break;
            case PropertyType.Fire:

                break;
            case PropertyType.Boom:

                break;
        }

        _effectSO.ApplyEffect(target);
    }

}
