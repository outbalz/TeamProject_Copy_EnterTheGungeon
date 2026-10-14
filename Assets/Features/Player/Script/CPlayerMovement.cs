using UnityEngine;
using UnityEngine.InputSystem;

public class CPlayerMovement : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private float _moveSpeed = 5.0f;
    #endregion

    #region 내부 변수
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;
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

    private void OnEnable()
    {
        if (_moveAction == null)
        {
            Debug.LogError("Move Action 참조가 비어있음");
            enabled = false;
            return;
        }

        if (_moveAction.action == null)
        {
            Debug.LogError("Move Action이 설정되지 않음");
            enabled = false;
            return;
        }

        _moveAction.action.Enable();
    }

    private void OnDisable()
    {
        if (_moveAction != null && _moveAction.action != null)
            _moveAction.action.Disable();

        _moveDirection = Vector2.zero;
    }

    private void Update()
    {
        InputMove();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // 이동 입력
    private void InputMove()
    {
        _moveDirection = _moveAction.action.ReadValue<Vector2>();

        _moveDirection = Vector2.ClampMagnitude(_moveDirection, 1.0f);
    }

    // 이동
    private void Move()
    {
        Vector2 nextPosition = _rigidbody.position + _moveDirection * _moveSpeed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(nextPosition);
    }
}
