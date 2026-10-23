
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/EntityDataSO/EntityStrategy/EntityAmmoCrateSO", fileName = "EntityStrategy_AmmoCrateSO")]
public class CEntityAmmoCrateSO : CEntityConsumeOnGetSO
{
    public override bool InteractCheck(CEntityController entity)
    {
        //TODO 현재 총의 탄약이 최대치인지 확인

        Destroy(entity);
        return true;
    }

    protected override void GetItem()
    {
        //TODO 탄약 보충기능

    }
}
