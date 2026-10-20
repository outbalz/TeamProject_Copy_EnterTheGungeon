using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CModifier
{
	#region 인스팩터
	[SerializeField] protected float _modifier;
    [SerializeField] protected string _modifierName;
	#endregion

    public string ModifierName => _modifierName;
    public float ModifierValue => _modifier;

	public abstract float GetModifiedValue(float baseValue);

    public CModifier(string name,float modifier) 
    {
        _modifierName = name;
        _modifier = modifier;
    }
}

[Serializable]
public class CAddModifier : CModifier
{

    public override float GetModifiedValue(float baseValue)
    {
		return baseValue + _modifier;
    }

    public CAddModifier(string name, float modifier) : base(name, modifier)
    {
    }
}


[Serializable]
public class CMultiflyModifier : CModifier
{

    public override float GetModifiedValue(float baseValue)
    {
        return baseValue * _modifier;
    }    
    
    public CMultiflyModifier(string name, float modifier) : base(name, modifier)
    {
    }

}