using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/EntityDataSO/EntityStrategy/EntitySupplyDropSO", fileName = "EntitySupplyDropSO_")]
public class CEntitySupplyDropSO : CEntityStrategySO
{
    #region 인스팩터
    [SerializeField] private CEntityDataSO _dropEntitiy;
    [SerializeField] private float _dropTime;
    [SerializeField] private AnimatorController _dropAnimation;
    #endregion

    public override void InitializeEntitiy(CEntityController entity)
    {
        //TODO 보급투하 애니메이션 실행
        entity.EntityAnimator.runtimeAnimatorController = _dropAnimation;
        DropEntitiy(entity).Forget();
    }

    public override bool InteractCheck(CEntityController entity)
    {
        return false;
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
        dropEnitiy.transform.position = entity.transform.position;

        dropEnitiy.OnDestroyEvent += () => Destroy(entity.gameObject);
    }

}
