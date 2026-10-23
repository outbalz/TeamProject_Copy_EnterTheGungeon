using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTripleShotPattern : CEnemyAttackPattern
{
    [Header("총알 설정")]
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private int _shotCount = 5;
    [SerializeField] private float _fireInterval = 0.5f;
    [SerializeField] private float _spreadAngle = 30f;

    [Header("발사 위치")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _player;

    private bool _isFiring = false;

    public override void Attack()
    {
        if (_isFiring)
        {
            return;
        }

        StartCoroutine(CoFire());
    }

    private IEnumerator CoFire()
    {
        _isFiring = true;

        for (int i = 0; i < _shotCount; i++)
        {
            if (_firePoint == null || _player == null)
            {
                break;
            }

            Vector2 direction = ((Vector2)_player.position - (Vector2)_firePoint.position).normalized;

            for (int j = -1; j <= 1; j++)
            {
                float angle = j * _spreadAngle;

                Vector2 bulletDirection =
                    Quaternion.Euler(0f, 0f, angle) * direction;

                CEnemyBullet bullet = CEnemyBulletFactory.Instance.Creat(
                    _firePoint.position,
                    Quaternion.identity
                );

                if (bullet != null)
                {
                    bullet.Fire(bulletDirection, _bulletSpeed);
                }
            }

            yield return new WaitForSeconds(_fireInterval);
        }

        _isFiring = false;
    }
}
