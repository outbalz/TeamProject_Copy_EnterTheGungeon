using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class CPlayerHPHandler : CHPHandler
{
    #region 인스펙터
    [SerializeField] private float _maxHP = 3.0f;

    [SerializeField] private float _hitDuration = 0.15f;
    [SerializeField] private float _hitInvincibleDuration = 1.0f;
    #endregion

    #region 내부 변수
    private CPlayerStateController _stateController;
    private CPlayerMovement _movement;
    private CPlayerRoll _roll;

    private bool _isHitInvincible;
    #endregion

    #region 프로퍼티
    public float MaxHP => _maxHP;
    public bool IsInvincible => _isHitInvincible ||
        (_roll != null && _roll.IsInvincible);
    #endregion

    private void Awake()
    {
        _stateController = GetComponent<CPlayerStateController>();
        _movement = GetComponent<CPlayerMovement>();
        _roll = GetComponent<CPlayerRoll>();

        if (_stateController == null || _movement == null || _roll == null)
        {
            Debug.LogError("플레이어 HP 관련 컴포넌트가 비어있음");
            enabled = false;
            return;
        }

        HP = _maxHP;
    }

    // 플레이어 피격
    public override UniTask OnHit(float damage)
    {
        if (!isActiveAndEnabled || damage <= 0.0f ||
            HP <= 0.0f || IsInvincible)
        {
            return UniTask.CompletedTask;
        }

        HP = Mathf.Max(0.0f, HP - damage);

        if (HP <= 0.0f)
        {
            _stateController.ChangeState(EPlayerState.Dead);
            OnDeathInvoke();

            return UniTask.CompletedTask;
        }

        _isHitInvincible = true;

        _stateController.ChangeState(EPlayerState.Hit);
        StartCoroutine(CoHit());

        return UniTask.CompletedTask;
    }

    // 피격 상태 및 무적 시간
    private IEnumerator CoHit()
    {
        yield return new WaitForSeconds(_hitDuration);

        if (_stateController.CurrentState == EPlayerState.Hit)
        {
            if (_movement.MoveDirection == Vector2.zero)
            {
                _stateController.ChangeState(EPlayerState.Idle);
            }
            else
            {
                _stateController.ChangeState(EPlayerState.Move);
            }
        }

        float remainTime = Mathf.Max(
            0.0f, _hitInvincibleDuration - _hitDuration);

        yield return new WaitForSeconds(remainTime);

        _isHitInvincible = false;
    }

    // 체력 회복
    public void Heal(float amount)
    {
        if (amount <= 0.0f || HP <= 0.0f)
        {
            return;
        }

        HP = Mathf.Min(_maxHP, HP + amount);
    }
}
