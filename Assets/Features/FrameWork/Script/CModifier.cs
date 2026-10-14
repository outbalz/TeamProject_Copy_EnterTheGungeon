using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CModifier
{
	#region 인스팩터
	[SerializeField] protected float _modifier;
	#endregion

	public abstract float GetModifiedValue(float baseValue);

    public CModifier(float modifier) 
    {
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

    public CAddModifier(float modifier) : base(modifier)
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
    
    public CMultiflyModifier(float modifier) : base(modifier)
    {
    }

}