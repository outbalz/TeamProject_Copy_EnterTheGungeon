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
    private string _currentAnimation;
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
                _spriteRenderer.flipX = false;
                _spriteRenderer.color = Color.white;

                _animator.Play("Dead_Front", 0, 0.0f);

                _wasRolling = false;
                _playedDead = true;
            }

            return;
        }

        // 구르기 애니메이션
        if (state == EPlayerState.Roll)
        {
            if (!_wasRolling)
            {
                PlayRollAnimation();

                _wasRolling = true;
                _currentAnimation = null;
            }

            return;
        }

        // 피격 중에는 현재 애니메이션 유지
        if (state == EPlayerState.Hit)
        {
            return;
        }

        if (_wasRolling)
        {
            _wasRolling = false;
            _currentAnimation = null;
        }

        string directionName = "Front";
        bool flipX = false;

        // 쉬프트 누르는 동안 마우스 방향 확인
        if (Input.GetKey(KeyCode.LeftShift) && Camera.main != null)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (Vector2)mouseWorld - (Vector2)transform.position;

            if (direction.sqrMagnitude > 0.01f)
            {
                direction.Normalize();

                bool isSide = Mathf.Abs(direction.x) > 0.35f;
                bool isBack = direction.y > 0.0f;

                if (isSide)
                {
                    if (isBack)
                    {
                        directionName = "BackRight";
                    }
                    else
                    {
                        directionName = "FrontRight";
                    }

                    flipX = direction.x < 0.0f;
                }
                else
                {
                    if (isBack)
                    {
                        directionName = "Back";
                    }
                    else
                    {
                        directionName = "Front";
                    }
                }
            }
        }

        _spriteRenderer.flipX = flipX;

        string animationName;

        if (state == EPlayerState.Move)
        {
            animationName = "Run_" + directionName;
        }
        else
        {
            animationName = "Idle_" + directionName;
        }

        // 애니메이션이 변경될 때만 재생
        if (_currentAnimation != animationName)
        {
            _animator.Play(animationName, 0, 0.0f);

            _currentAnimation = animationName;
        }
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
