using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEntitySupplyDropSO : CEntityStrategySO
{
    #region 인스팩터
    [SerializeField] private CEntityDataSO _dropEntitiy;
    [SerializeField] private float _dropTime;
    #endregion

    public override void InitializeEntitiy(CEntityController entity)
    {
        //TODO 보급투하 애니메이션 실행

        DropEntitiy(entity).Forget();
    }

    private async UniTask DropEntitiy(CEntityController entity)
    {
        float timer = _dropTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            await UniTask.NextFrame(PlayerLoopTiming.Update);
        }

        CEntityController dropEnitiy = CEntityFactory.Instance.Creat(_dropEntitiy);

        dropEnitiy.OnDestroyEvent += () => Destroy(entity.gameObject);
    }

}
