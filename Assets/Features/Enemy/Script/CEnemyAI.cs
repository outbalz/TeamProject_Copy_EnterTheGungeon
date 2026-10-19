using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyAI : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] private Transform _player;

    [Header("감지")]
    [SerializeField] private float _detectRange = 8f;

    [Header("공격")]
    [SerializeField] private float _attackRange = 6f;
    [SerializeField] private float _attackCoolTime = 6f;
    [SerializeField] private CEnemyAttackPattern _attackPattern;

    private float _attackTimer = 0f;

    [Header("이동")]
    [SerializeField] private CEnemyMovement _enemyMovement;

    private void Update()
    {
        if (_player == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, _player.position);

        if (distance > _detectRange)
        {
            if (_enemyMovement != null)
            {
                _enemyMovement.StopMove();
            }

            return;
        }

        if (distance <= _attackRange)
        {
            if (_enemyMovement != null)
            {
                _enemyMovement.MoveSideways();
            }

            _attackTimer += Time.deltaTime;

            if (_attackTimer >= _attackCoolTime)
            {
                if (_attackPattern != null)
                {
                    _attackPattern.Attack();
                }

                _attackTimer = 0f;
            }

            return;
        }

        if (_enemyMovement != null)
        {
            _enemyMovement.MoveToTarget(_player);
        }
    }
}
