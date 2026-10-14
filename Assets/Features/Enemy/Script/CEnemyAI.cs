using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyAI : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] private Transform _player;

    [Header("이동")]
    // 적 이동 속도
    [SerializeField] private float _moveSpeed = 2f;

    [Header("감지")]
    // 플레이어 탐지 거리
    [SerializeField] private float _detectRange = 8f;

    [Header("공격")]
    // 적 공격 사거리
    [SerializeField] private float _attackRange = 6f;
    // 공격 쿨타임
    [SerializeField] private float _attackCoolTime = 2f;

    #region
    // 내부 변수


    #endregion


    private void Update()
    {
        if (_player == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, _player.position);

        if (distance <= _detectRange)
        {
            if (distance > _attackRange)
            {
                Vector2 direction = _player.position - transform.position;
                direction.Normalize();

                transform.position += (Vector3)(direction * _moveSpeed * Time.deltaTime);
            }
        }
    }
}
