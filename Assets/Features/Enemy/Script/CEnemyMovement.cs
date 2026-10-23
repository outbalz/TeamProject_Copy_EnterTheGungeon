using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMovement : MonoBehaviour
{
    public enum EEnemyMovePattern
    {
        Chase,
        Strafe,
        KeepDistance,
        Circle,
        Stop
    }

    [Header("이동 패턴")]
    [SerializeField]
    private EEnemyMovePattern _movePattern =
    EEnemyMovePattern.Strafe;

    [Header("이동 설정")]
    [SerializeField] private float _moveSpeed = 2f;

    [Header("거리 설정")]
    [SerializeField] private float _preferredDistance = 5f;
    [SerializeField] private float _distanceTolerance = 0.5f;

    [Header("방향 전환")]
    [SerializeField] private float _changeDirectionTime = 1.5f;

    private Rigidbody2D _rigidbody2D;
    private float _directionTimer = 0f;
    private int _moveDirection = 1;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void MoveByPattern(Transform target, float attackRange)
    {
        if (target == null || _rigidbody2D == null)
        {
            return;
        }

        Vector2 toTarget =
            (Vector2)target.position - _rigidbody2D.position;

        float distance = toTarget.magnitude;

        Vector2 direction = Vector2.zero;

        if (distance > 0f)
        {
            direction = toTarget / distance;
        }

        if (_movePattern == EEnemyMovePattern.Chase)
        {
            if (distance > attackRange)
            {
                MoveInDirection(direction);
            }
            else
            {
                StopMove();
            }
        }
        else if (_movePattern == EEnemyMovePattern.Strafe)
        {
            if (distance > attackRange)
            {
                MoveInDirection(direction);
            }
            else
            {
                MoveSideways(direction);
            }
        }
        else if (_movePattern == EEnemyMovePattern.KeepDistance)
        {
            KeepDistance(distance, direction);
        }
        else if (_movePattern == EEnemyMovePattern.Circle)
        {
            MoveCircle(distance, direction);
        }
        else if (_movePattern == EEnemyMovePattern.Stop)
        {
            StopMove();
        }
    }

    private void MoveInDirection(Vector2 direction)
    {
        _rigidbody2D.velocity = direction * _moveSpeed;
    }

    private void MoveSideways(Vector2 toTarget)
    {
        UpdateMoveDirection();

        Vector2 sideDirection =
            new Vector2(-toTarget.y, toTarget.x);

        sideDirection *= _moveDirection;

        MoveInDirection(sideDirection);
    }

    private void KeepDistance(float distance, Vector2 toTarget)
    {
        if (distance > _preferredDistance + _distanceTolerance)
        {
            MoveInDirection(toTarget);
        }
        else if (distance < _preferredDistance - _distanceTolerance)
        {
            MoveInDirection(-toTarget);
        }
        else
        {
            StopMove();
        }
    }

    private void MoveCircle(float distance, Vector2 toTarget)
    {
        UpdateMoveDirection();

        Vector2 sideDirection =
            new Vector2(-toTarget.y, toTarget.x);

        sideDirection *= _moveDirection;

        Vector2 direction = sideDirection;

        if (distance > _preferredDistance + _distanceTolerance)
        {
            direction += toTarget;
        }
        else if (distance < _preferredDistance - _distanceTolerance)
        {
            direction -= toTarget;
        }

        MoveInDirection(direction.normalized);
    }

    private void UpdateMoveDirection()
    {
        _directionTimer += Time.deltaTime;

        if (_directionTimer >= _changeDirectionTime)
        {
            _moveDirection *= -1;
            _directionTimer = 0f;
        }
    }

    public void StopMove()
    {
        if (_rigidbody2D != null)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    public void MoveToTarget(Transform target)
    {
        if (target == null || _rigidbody2D == null)
        {
            return;
        }

        Vector2 direction =
            (Vector2)target.position - _rigidbody2D.position;

        MoveInDirection(direction.normalized);
    }

    public void MoveSideways()
    {
        if (_rigidbody2D == null)
        {
            return;
        }

        UpdateMoveDirection();

        MoveInDirection(new Vector2(_moveDirection, 0f));
    }
} 
