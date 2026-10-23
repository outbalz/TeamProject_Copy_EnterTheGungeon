using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 충돌시 피해 기능 넣기

public class CEnemyBullet : MonoBehaviour
{
    [Header("총알 설정")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _lifeTime = 5f;

    private Rigidbody2D _rigidbody2D;
    private float _lifeTimer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _lifeTimer = 0f;
    }

    private void Update()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _lifeTime)
        {
            ReturnToPool();
        }
    }

    public void Fire(Vector2 direction, float speed)
    {
        if (_rigidbody2D == null)
        {
            return;
        }

        _moveSpeed = speed;
        _rigidbody2D.velocity = direction.normalized * _moveSpeed;
    }

    private void ReturnToPool()
    {
        CEnemyBulletPool.Instance.ReturnToPool(this);
    }

    private void OnDisable()
    {
        if (_rigidbody2D != null)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
} 
