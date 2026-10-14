using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CSingletonExample : CSingleton<CSingletonExample>
{
    protected override void Awake()
    {
        base.Awake();


        //Do something
    }

    public void Test()
    {
        Debug.LogWarning("TestMassage");
    }
}
