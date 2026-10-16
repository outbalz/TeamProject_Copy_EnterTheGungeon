using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFactoryExample : CFactory<CFactoryExample, CTest>
{
    #region 인스팩터
    [SerializeField] private float _scaleModifier = 0.5f;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        if(_scaleModifier <= 0)
        {
            Debug.LogWarning("_scaleModifier가 너무 작습니다.");
            Mathf.Max(0.01f, _scaleModifier);
        }
    }

    public override CTest Creat()
    {
        CTest test = Instantiate(_prefab);

        test.transform.localScale = Vector3.one * _scaleModifier;

        return test;
    }
}
