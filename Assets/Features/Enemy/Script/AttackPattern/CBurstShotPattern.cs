using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBurstShotPattern : CEnemyAttackPattern
{
    [Header("총알 설정")]
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private int _bulletCount = 15;
    [SerializeField] private float _fireInterval = 0.2f;

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

        for (int i = 0; i < _bulletCount; i++)
        {
            if (_firePoint == null || _player == null)
            {
                break;
            }

            CEnemyBullet bullet = CEnemyBulletFactory.Instance.Creat(
                    _firePoint.position,
                    Quaternion.identity
                );

            if (bullet != null)
            {

                Vector2 direction = (Vector2)(_player.position - _firePoint.position);

                bullet.Fire(direction, _bulletSpeed);
            }

            yield return new WaitForSeconds(_fireInterval);
        }

        _isFiring = false;
    }
}
