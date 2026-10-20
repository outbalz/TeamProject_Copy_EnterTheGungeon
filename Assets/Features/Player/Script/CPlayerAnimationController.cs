using UnityEngine;

public class CPlayerAnimationController : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private SpriteMask _hitMask;
    [SerializeField] private SpriteRenderer _whiteFlashRenderer;
    #endregion

    #region 내부 변수
    private CPlayerStateController _stateController;
    private CPlayerRoll _roll;
    private CPlayerHPHandler _hpHandler;

    private bool _wasRolling;
    private bool _playedDead;
    private bool _wasHit;

    private float _hitFlashTimer;
    #endregion
    private void Awake()
    {
        _stateController = GetComponent<CPlayerStateController>();
        _roll = GetComponent<CPlayerRoll>();
        _hpHandler = GetComponent<CPlayerHPHandler>();

        if (_animator == null || _spriteRenderer == null ||
            _stateController == null || _roll == null ||
            _hpHandler == null || _hitMask == null ||
            _whiteFlashRenderer == null)
        {
            Debug.LogError("플레이어 애니메이션 참조가 비어있음");
            enabled = false;
            return;
        }

        if (_whiteFlashRenderer.sprite == null)
        {
            Debug.LogError("WhiteFlash 스프라이트가 비어있음");
            enabled = false;
            return;
        }

        // 흰색 점멸 초기 설정
        _whiteFlashRenderer.color = Color.white;
        _whiteFlashRenderer.enabled = false;

        _whiteFlashRenderer.sortingLayerID = _spriteRenderer.sortingLayerID;

        _whiteFlashRenderer.sortingOrder =  _spriteRenderer.sortingOrder + 1;

        _whiteFlashRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        // SpriteMask 적용 범위
        _hitMask.isCustomRangeActive = true;

        _hitMask.frontSortingLayerID = _spriteRenderer.sortingLayerID;

        _hitMask.backSortingLayerID =_spriteRenderer.sortingLayerID;

        _hitMask.frontSortingOrder = _spriteRenderer.sortingOrder + 2;

        _hitMask.backSortingOrder = _spriteRenderer.sortingOrder;
    }

    private void Update()
    {
        UpdateAnimation();
        UpdateHitEffect();
    }
    private void LateUpdate()
    {
        UpdateHitMask();
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


    // 피격 및 무적 효과
    private void UpdateHitEffect()
    {
        EPlayerState state = _stateController.CurrentState;

        if (state == EPlayerState.Dead)
        {
            _whiteFlashRenderer.enabled = false;
            _spriteRenderer.color = Color.white;
            _wasHit = false;
            return;
        }

        Color color = Color.white;

        // 피격 직후 흰색 점멸
        if (state == EPlayerState.Hit)
        {
            if (!_wasHit)
            {
                _hitFlashTimer = 0.0f;
                _wasHit = true;
            }

            _hitFlashTimer += Time.deltaTime;

            int flashCount = Mathf.FloorToInt(_hitFlashTimer / 0.05f);

            _whiteFlashRenderer.enabled = flashCount % 2 == 0;

            _spriteRenderer.color = Color.white;
            return;
        }

        _wasHit = false;
        _whiteFlashRenderer.enabled = false;

        // 피격 후 무적 시간
        if (_hpHandler.IsHitInvincible)
        {
            if (Mathf.PingPong(Time.time * 6.0f, 1.0f) < 0.5f)
            {
                color.a = 0.4f;
            }
        }

        _spriteRenderer.color = color;
    }


    // 현재 캐릭터 스프라이트로 마스크 갱신
    private void UpdateHitMask()
    {
        if (!_whiteFlashRenderer.enabled)
        {
            return;
        }

        Sprite sprite = _spriteRenderer.sprite;
        Sprite whiteSprite = _whiteFlashRenderer.sprite;

        if (sprite == null || whiteSprite == null)
        {
            return;
        }

        _hitMask.sprite = sprite;

        // 캐릭터 좌우 반전 적용
        if (_spriteRenderer.flipX)
        {
            _hitMask.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            _hitMask.transform.localScale = Vector3.one;
        }

        // 흰색 사각형 크기 조절
        Vector3 scale =  _whiteFlashRenderer.transform.localScale;

        scale.x = sprite.bounds.size.x /  whiteSprite.bounds.size.x;

        scale.y = sprite.bounds.size.y / whiteSprite.bounds.size.y;

        _whiteFlashRenderer.transform.localScale = scale;
    }
}
