using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpiralShotPattern : CEnemyAttackPattern
{
    [Header("발사 설정")]
    [SerializeField] private int _bulletCount = 8;
    [SerializeField] private float _fireInterval = 0.2f;
    [SerializeField] private float _bulletSpeed = 5f;

    [Header("회전 설정")]
    [SerializeField] private float _rotateAngle = 10f;
    [SerializeField] private float _fireDuration = 5f;

    private bool _isFiring = false;
    private float _currentAngle = 0f;

    public override void Attack()
    {
        if (_isFiring)
        {
            return;
        }

        StartCoroutine(CoFireSpiral());
    }

    private IEnumerator CoFireSpiral()
    {
        _isFiring = true;

        float timer = 0f;

        while (timer < _fireDuration)
        {
            for (int i = 0; i < _bulletCount; i++)
            {
                float angle = _currentAngle + (360f / _bulletCount) * i;

                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                Vector2 direction = new Vector2(x, y);

                CEnemyBullet bullet = CEnemyBulletFactory.Instance.Creat(
                    transform.position,
                    Quaternion.identity
                );

                if (bullet != null)
                {
                    bullet.Fire(direction, _bulletSpeed);
                }
            }

            _currentAngle += _rotateAngle;

            yield return new WaitForSeconds(_fireInterval);

            timer += _fireInterval;
        }

        _isFiring = false;
    }
}
