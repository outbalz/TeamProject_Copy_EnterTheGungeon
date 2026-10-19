using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpreadShotPattern : CEnemyAttackPattern
{
    [Header("ÃÑ¾Ë ¼³Á¤")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _player;

    [Header("»êÅº ¼³Á¤")]
    [SerializeField] private int _bulletCount = 5;
    [SerializeField] private float _spreadAngle = 60f;

    public override void Attack()
    {
        if (_bulletPrefab == null || _firePoint == null || _player == null)
        {
            return;
        }

        Vector2 direction = ((Vector2)_player.position - (Vector2)_firePoint.position).normalized;

        float startAngle = -_spreadAngle / 2f;
        float angleStep = _spreadAngle / (_bulletCount - 1);

        for (int i = 0; i < _bulletCount; i++)
        {
            float angle = startAngle + angleStep * i;

            Vector2 bulletDirection = Quaternion.Euler(0f, 0f, angle) * direction;

            GameObject bulletObject = Instantiate(
                _bulletPrefab,
                _firePoint.position,
                Quaternion.identity
            );

            CEnemyBullet bullet = bulletObject.GetComponent<CEnemyBullet>();

            if (bullet != null)
            {
                bullet.Fire(bulletDirection);
            }
        }
    }
}
