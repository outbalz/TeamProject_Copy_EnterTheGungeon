using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/ItemDataSO/ActiveItemEffect/EntityDrop", fileName = "ActiveItemEntityDropSO_")]
public class CActiveItemEntityDropSO : CActiveItemEffectStrategySO
{
    #region 인스팩터
    [SerializeField] private CEntityDataSO _entity;
    #endregion


    public override UniTask OnUse()
    {
        CEntityController entity = CEntityFactory.Instance.Creat(_entity);
        entity.transform.position = CPlayerCharacterController.Instance.transform.position;

        return UniTask.CompletedTask;
    }
}
