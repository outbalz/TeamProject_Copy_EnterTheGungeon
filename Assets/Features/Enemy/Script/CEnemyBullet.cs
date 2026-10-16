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

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    public void Fire(Vector2 direction)
    {
        if (_rigidbody2D == null)
        {
            return;
        }

        _rigidbody2D.velocity = direction.normalized * _moveSpeed;
    }
}
