using UnityEngine;

public class CPlayerAnimationController : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    #endregion

    #region 내부 변수
    private CPlayerStateController _stateController;
    private CPlayerRoll _roll;

    private bool _wasRolling;
    private bool _playedDead;
    #endregion

    private void Awake()
    {
        _stateController = GetComponent<CPlayerStateController>();
        _roll = GetComponent<CPlayerRoll>();

        if (_animator == null || _spriteRenderer == null ||
            _stateController == null || _roll == null)
        {
            Debug.LogError("플레이어 애니메이션 참조가 비어있음");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        UpdateAnimation();
    }

    // 플레이어 상태에 따른 애니메이션 변경
    private void UpdateAnimation()
    {
        EPlayerState state = _stateController.CurrentState;


        // 사망 애니메이션
        if (state == EPlayerState.Dead)
        {
            if (!_playedDead)
            {
                _animator.SetBool("IsMoving", false);

                _spriteRenderer.flipX = false;
                _spriteRenderer.color = Color.white;

                _animator.Play("Dead_Front", 0, 0.0f);

                _wasRolling = false;
                _playedDead = true;
            }

            return;
        }
        
        if (state == EPlayerState.Roll)
        {
            if (!_wasRolling)
            {
                _animator.SetBool("IsMoving", false);

                PlayRollAnimation();

                _wasRolling = true;
            }

            return;
        }

        if (_wasRolling)
        {
            _wasRolling = false;
            _spriteRenderer.flipX = false;

            if (state == EPlayerState.Move)
            {
                _animator.Play("Run_Front", 0, 0.0f);
            }
            else
            {
                _animator.Play("Idle_Front", 0, 0.0f);
            }
        }

        _animator.SetBool("IsMoving", state == EPlayerState.Move);
    }

    // 구르기 방향에 따른 애니메이션
    private void PlayRollAnimation()
    {
        Vector2 direction = _roll.RollDirection;

        bool isSide = Mathf.Abs(direction.x) > 0.25f;
        bool isBack = direction.y > 0.25f;

        _spriteRenderer.flipX = direction.x < 0.0f;

        if (isSide)
        {
            if (isBack)
            {
                _animator.Play("Roll_BackRight", 0, 0.0f);
            }
            else
            {
                _animator.Play("Roll_FrontRight", 0, 0.0f);
            }
        }
        else
        {
            _spriteRenderer.flipX = false;

            if (isBack)
            {
                _animator.Play("Roll_Back", 0, 0.0f);
            }
            else
            {
                _animator.Play("Roll_Front", 0, 0.0f);
            }
        }
    }
}
