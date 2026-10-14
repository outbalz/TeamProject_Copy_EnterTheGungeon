using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CProjectileDataSO : CGameDataSO
{
    #region 인스팩터

    #endregion

    #region 프로퍼티

    #endregion

    //투사체가 벽이나 오브젝트에 충돌했을 경우 호출
    public abstract UniTask OnImpact();

    //투사체가 케릭터에게 맞았을 때 호출
    public abstract UniTask OnHit();
}
