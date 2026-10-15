using Cysharp.Threading.Tasks;
using UnityEngine;

public class CEnemyHealth : CHPHandler
{
    [Header("체력 설정")]
    [SerializeField] private float _maxHealth = 100f;


    void Start()
    {
        HP = _maxHealth;
    }
    public override UniTask OnHit(float damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            HP = 0;
            OnDeathInvoke();
        }

        return UniTask.CompletedTask;
    }
}
