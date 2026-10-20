using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMovement : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float _moveSpeed = 2f;

    [Header("좌우 이동 설정")]
    [SerializeField] private float _sideMoveSpeed = 1f;
    [SerializeField] private float _changeDirectionTime = 1f;

    private Rigidbody2D _rigidbody2D;
    private float _directionTimer;
    private int _moveDirection = 1;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void MoveToTarget(Transform target)
    {
        if (target == null || _rigidbody2D == null)
        {
            return;
        }

        Vector2 direction = ((Vector2)target.position - _rigidbody2D.position).normalized;

        _rigidbody2D.velocity = direction * _moveSpeed;
    }

    public void MoveSideways()
    {
        if (_rigidbody2D == null)
        {
            return;
        }

        _directionTimer += Time.deltaTime;

        if (_directionTimer >= _changeDirectionTime)
        {
            _moveDirection *= -1;
            _directionTimer = 0f;
        }

        Vector2 direction = new Vector2(_moveDirection, 0f);

        _rigidbody2D.velocity = direction * _sideMoveSpeed;
    }

    public void StopMove()
    {
        if (_rigidbody2D != null)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}
