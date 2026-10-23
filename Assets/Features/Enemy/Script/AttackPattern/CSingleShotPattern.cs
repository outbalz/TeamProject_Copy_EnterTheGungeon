using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSingleShotPattern : CEnemyAttackPattern
{
    [Header("총알 설정")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _player;
    [SerializeField] private float _bulletSpeed = 5f;

    [Header("탄창 설정")]
    [SerializeField] private int _magazineSize = 6;
    [SerializeField] private float _fireInterval = 0.2f;
    [SerializeField] private float _reloadTime = 2f;

    private bool _isAttacking = false;

    public override void Attack()
    {
        if (_isAttacking)
        {
            return;
        }

        StartCoroutine(CoAttack());
    }

    private IEnumerator CoAttack()
    {
        _isAttacking = true;

        for (int i = 0; i < _magazineSize; i++)
        {
            if (_bulletPrefab == null || _firePoint == null || _player == null)
            {
                break;
            }

            GameObject bulletObject = Instantiate(
                _bulletPrefab,
                _firePoint.position,
                Quaternion.identity
            );

            CEnemyBullet bullet = bulletObject.GetComponent<CEnemyBullet>();

            if (bullet != null)
            {
                Vector2 direction =
                    (Vector2)(_player.position - _firePoint.position);

                bullet.Fire(direction, _bulletSpeed);
            }

            if (i < _magazineSize - 1)
            {
                yield return new WaitForSeconds(_fireInterval);
            }
        }

        yield return new WaitForSeconds(_reloadTime);

        _isAttacking = false;
    }
} 
