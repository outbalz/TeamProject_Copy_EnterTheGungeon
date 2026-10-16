using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMovement : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float _moveSpeed = 2f;

    private Rigidbody2D _rigidbody2D;

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

    public void StopMove()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }
}
