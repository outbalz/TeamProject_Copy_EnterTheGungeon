using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CModifierHandler : MonoBehaviour
{
	#region 내부변수
	private Dictionary<string, List<CModifier>> _modifierMap = new Dictionary<string, List<CModifier>>();
	#endregion

	public void ApplyModifier(CModifier modifier)
	{
		if (_modifierMap.ContainsKey(modifier.ModifierName))
		{
			_modifierMap[modifier.ModifierName].Add(modifier);
		}

		else
		{
			_modifierMap.Add(modifier.ModifierName, new List<CModifier> { modifier });
		}

	}

	public void RemoveModifier(CModifier modifier)
	{
		if (!_modifierMap.ContainsKey(modifier.ModifierName))
		{
			return;
		}

		if (_modifierMap[modifier.ModifierName].Contains(modifier))
		{
			_modifierMap[modifier.ModifierName].Remove(modifier);
		}
	}

	public void RemoveModifier(string modifierName)
	{
		if (!_modifierMap.ContainsKey(modifierName))
		{
			return;
		}

		_modifierMap[modifierName] = null;
		_modifierMap.Remove(modifierName);
	}

	public float GetModifiedValue(string modifierName, float baseValue)
	{
		if (!_modifierMap.ContainsKey(modifierName))
		{
			return baseValue;
		}

		List<CModifier> modifiers = _modifierMap[modifierName];
		float resultValue = baseValue;

		for (int i = 0; i < modifiers.Count; i++)
		{
			resultValue = modifiers[i].GetModifiedValue(resultValue);
		}

		return resultValue;
	}
}
