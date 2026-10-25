using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/EntityDataSO/EntityStrategy/TrowableEnitySO", fileName = "TrowableEnitySO_")]
public class CTrowableEnitySO : CEntityStrategySO
{
    #region 인스팩터
    [SerializeField] private float _force;
    [SerializeField] private float _lifeTime;
    [SerializeField] private CEntityDestroyStrategySO _destroyStrategy;
    #endregion

    #region 프로퍼티
    public float Force => _force;
    #endregion

    public override void InitializeEntitiy(CEntityController entity)
    {
        EntityLifeUpdate(entity).Forget();
    }

    public override bool InteractCheck(CEntityController entity)
    {
        return false;
    }

    private async UniTask EntityLifeUpdate(CEntityController entity)
    {
        float timer = _lifeTime;
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            await UniTask.NextFrame(PlayerLoopTiming.Update);
        }

        _destroyStrategy.OnEnitityDestoryed();
        Destroy(entity.gameObject);
    }
}
