using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/ItemDataSO/ActiveItemEffect/EntityTrow", fileName = "ActiveItemEntityTrowSO_")]
public class CActiveItemEntityTrowSO : CActiveItemEffectStrategySO
{
    #region 인스팩터
    [SerializeField] private CEntityDataSO _entity;
    #endregion

    public override UniTask OnUse()
    {
        if(_entity.Strategy == null)
        {
            return UniTask.CompletedTask;
        }

        if(!(_entity.Strategy is CTrowableEnitySO trowable))
        {
            return UniTask.CompletedTask;
        }

        Vector2 pos = CPlayerCharacterController.Instance.transform.position;

        //조준 데이터를 받아오기 전까지 임시로 사용할 데이터
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        Vector2 velocity = (pos - mousePos).normalized * trowable.Force;

        CEntityController entity = CEntityFactory.Instance.CreatAndTrow(velocity, _entity, pos);

        return UniTask.CompletedTask;
    }
}
