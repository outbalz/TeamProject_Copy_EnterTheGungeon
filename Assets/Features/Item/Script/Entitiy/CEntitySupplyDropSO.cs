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
    [SerializeField] private Vector2 _offset;
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
        Vector2 targetPos = entity.transform.position;

        entity.transform.position = targetPos + _offset;

        Vector2 startPos = entity.transform.position;

        float timer = _dropTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            float t = 1 - (timer / _dropTime);

            entity.transform.position = Vector3.Lerp(startPos, targetPos, t);

            await UniTask.NextFrame(PlayerLoopTiming.Update);
        }

        CEntityController dropEnitiy = CEntityFactory.Instance.Creat(_dropEntitiy, entity.transform.position);

        dropEnitiy.OnDestroyEvent += () => Destroy(entity.gameObject);
    }

}
