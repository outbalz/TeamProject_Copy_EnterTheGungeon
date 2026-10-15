using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActiveItemEntityDropSO : CActiveItemEffectStrategySO
{
    #region 인스팩터
    [SerializeField] private CEntityDataSO _entity;
    #endregion

    private CEntityFactory _entityFactory;

    public override UniTask OnUse()
    {
        CEntityController entity = _entityFactory.Creat(_entity);
        entity.transform.position = CPlayerCharacterController.Instance.transform.position;

        return UniTask.CompletedTask;
    }
}
