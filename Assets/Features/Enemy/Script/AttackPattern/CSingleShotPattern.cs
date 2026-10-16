using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSingleShotPattern : CEnemyAttackPattern
{
    [Header("ÃÑ¾Ë ¼³Á¤")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _player;

    public override void Attack()
    {
        if (_bulletPrefab == null || _firePoint == null || _player == null)
        {
            return;
        }

        GameObject bulletObject = Instantiate(
            _bulletPrefab,
            _firePoint.position,
            Quaternion.identity
        );

        CEnemyBullet bullet = bulletObject.GetComponent<CEnemyBullet>();

        if (bullet != null)
        {
            Vector2 direction = (Vector2)(_player.position - _firePoint.position);
            bullet.Fire(direction);
        }
    }
}
