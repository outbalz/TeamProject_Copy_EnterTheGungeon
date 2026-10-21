using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Progress;

public class CTrowableEnitiySO : CEntityStrategySO
{
    #region 인스팩터
    [SerializeField] private float _force;
    [SerializeField] private float _lifeTime;
    [SerializeField] private CEntityDestroyStrategySO _destroyStrategy;
    #endregion

    public override void InitializeEntitiy(CEntityController entity)
    {
        EntityLifeUpdate(entity).Forget();
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
