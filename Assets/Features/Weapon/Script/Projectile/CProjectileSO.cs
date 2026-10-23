using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
[CreateAssetMenu(menuName = "Weapon/Projectile/CProjectileSO", fileName = "CProjectileSO")]
public class CProjectileSO : CProjectileDataSO
{
    public override async UniTask OnImpact()
    {

        await UniTask.CompletedTask;
    }


    public override async UniTask OnHit()
    {
        await OnImpact();
    }

}
