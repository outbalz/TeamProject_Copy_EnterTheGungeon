using UnityEngine;

public class CPlayerMovement : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] private string _moveActionName = "Move";
    [SerializeField] private float _moveSpeed = 5.0f;
    #endregion

    #region 내부 변수
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;
    private CInputVectorActionData _moveInput;
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody2D가 비어있음");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        CInputDispatcher dispatcher = FindObjectOfType<CInputDispatcher>();

        if (dispatcher == null)
        {
            Debug.LogError("InputDispatcher가 비어있음");
            enabled = false;
            return;
        }

        if (!dispatcher.InputVectorActionMap.TryGetValue(_moveActionName, out _moveInput))
        {
            Debug.LogError("Move Action이 없음");
            enabled = false;
            return;
        }

        _moveInput.OnPerformedAction += InputMove;
    }

    private void OnEnable()
    {
        if (_moveInput != null)
            _moveInput.OnPerformedAction += InputMove;
    }

    private void OnDisable()
    {
        if (_moveInput != null)
            _moveInput.OnPerformedAction -= InputMove;

        _moveDirection = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Move();
    }

    // 이동 입력
    private void InputMove(Vector2 direction)
    {
        _moveDirection = Vector2.ClampMagnitude(direction, 1.0f);
    }

    // 이동
    private void Move()
    {
        Vector2 nextPosition = _rigidbody.position + _moveDirection * _moveSpeed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(nextPosition);
    }
}