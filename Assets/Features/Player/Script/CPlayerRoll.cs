using UnityEngine;

public class CPlayerRoll : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] private string _rollActionName = "DodgeRoll";
    [SerializeField] private float _rollSpeed = 4.5f;
    [SerializeField] private float _rollDuration = 0.7f;
    #endregion

    #region 내부 변수
    private Rigidbody2D _rigidbody;
    private CPlayerMovement _movement;
    private CPlayerStateController _stateController;
    private CInputActionData _rollInput;
    private Vector2 _rollDirection;
    private float _rollTimer;
    #endregion

    #region 프로퍼티
    public bool IsInvincible =>
        _stateController != null &&
        _stateController.CurrentState == EPlayerState.Roll &&
        _rollTimer > _rollDuration * 0.5f;
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _movement = GetComponent<CPlayerMovement>();
        _stateController = GetComponent<CPlayerStateController>();

        if (_rigidbody == null || _movement == null || _stateController == null)
        {
            Debug.LogError("플레이어 구르기 컴포넌트가 비어있음");
            enabled = false;
            return;
        }
    }

    void Start()
    {
        CInputDispatcher dispatcher = CInputDispatcher.Instance;

        if (!dispatcher.InputActionMap.TryGetValue(_rollActionName, out _rollInput))
        {
            Debug.LogError("DodgeRoll Action이 없음");
            enabled = false;
            return;
        }

        _rollInput.OnPerformedAction += StartRoll;
    }

    private void OnEnable()
    {
        if (_rollInput != null)
        {
            _rollInput.OnPerformedAction += StartRoll;
        }
    }

    private void OnDisable()
    {
        if (_rollInput != null)
        {
            _rollInput.OnPerformedAction -= StartRoll;
        }

        if (_stateController != null &&
            _stateController.CurrentState == EPlayerState.Roll)
        {
            _stateController.ChangeState(EPlayerState.Idle);
        }
    }

    private void FixedUpdate()
    {
        if (_stateController.CurrentState != EPlayerState.Roll)
        {
            return;
        }

        Roll();
    }

    // 구르기 시작
    private void StartRoll()
    {
        if (_stateController.CurrentState != EPlayerState.Move)
        {
            return;
        }

        _rollDirection = _movement.MoveDirection.normalized;
        _rollTimer = _rollDuration;

        _stateController.ChangeState(EPlayerState.Roll);
    }

    // 구르기 이동
    private void Roll()
    {
        Vector2 nextPosition = _rigidbody.position + _rollDirection * _rollSpeed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(nextPosition);

        _rollTimer -= Time.fixedDeltaTime;

        if (_rollTimer <= 0.0f)
        {
            EndRoll();
        }
    }

    // 구르기 종료
    private void EndRoll()
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
}