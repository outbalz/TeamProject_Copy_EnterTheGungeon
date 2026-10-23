using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStatusEffectHandler : MonoBehaviour
{
    #region 인스팩터
    [SerializeField] private EStatusEffectType _effectType;
    #endregion

    #region 내부변수
    private Dictionary<string, List<CStatusEffectSO>> _statusEffectMap = new Dictionary<string, List<CStatusEffectSO>>();
	#endregion

    public bool ApplyStatusEffect(CStatusEffectSO effect)
    {
        if(effect == null)
        {
            Debug.LogWarning("잘못된 CStatusEffectSO 데이터 전달");
            return false;
        }

        if(_effectType != effect.EffectType)
        {
            return false;
        }

        if (_statusEffectMap.ContainsKey(effect.ID))
        {
            _statusEffectMap[effect.ID].Add(effect);
            effect.Strategy.OnApply(this);
            return true;
        }

        else
        {
            _statusEffectMap.Add(effect.ID, new List<CStatusEffectSO>{effect});
            effect.Strategy.OnApply(this);
            return true;
        }
    }

    public bool RemoveStatusEffect(CStatusEffectSO effect)
    {
        if (effect == null)
        {
            Debug.LogWarning("잘못된 CStatusEffectSO 데이터 전달");
            return false;
        }

        if (_effectType != effect.EffectType)
        {
            return false;
        }

        if (_statusEffectMap.ContainsKey(effect.ID))
        {
            _statusEffectMap[effect.ID].Remove(effect);
            effect.Strategy.OnRemoved(this);
            
            if(_statusEffectMap[effect.ID].Count <= 0)
            {
                _statusEffectMap[effect.ID] = null;
                _statusEffectMap.Remove(effect.ID);
            }

            return true;
        }

        else
        {
            return false;
        }
    }

}
