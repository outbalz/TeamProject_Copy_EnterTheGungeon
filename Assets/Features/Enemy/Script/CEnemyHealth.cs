using Cysharp.Threading.Tasks;
using UnityEngine;

public class CEnemyHealth : CHPHandler
{
    private void Start()
    {
        HP = MaxHP;
    }
    public override UniTask OnHit(float damage)
    {
        HP -= damage;

        OnHitInvoke();

        if (HP <= 0)
        {
            OnDeathInvoke();
        }

        return UniTask.CompletedTask;
    }
}
