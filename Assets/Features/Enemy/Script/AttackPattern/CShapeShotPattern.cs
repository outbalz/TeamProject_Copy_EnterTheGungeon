using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShapeShotPattern : CEnemyAttackPattern
{
    public enum EShotShape
    {
        Circle,
        Triangle,
        Square,
        XShape
    }

    [Header("모양 설정")]
    [SerializeField] private EShotShape _shotShape = EShotShape.Square;
    [SerializeField] private Transform _player;
    [SerializeField] private float _shapeSize = 2f;
    [SerializeField] private int _bulletCount = 16;

    [Header("총알 설정")]
    [SerializeField] private float _bulletSpeed = 5f;

    private Vector2 _throwDirection;

    public override void Attack()
    {
        if (_player == null)
        {
            return;
        }

        _throwDirection =
            ((Vector2)_player.position - (Vector2)transform.position).normalized;

        if (_throwDirection == Vector2.zero)
        {
            return;
        }

        if (_shotShape == EShotShape.Circle)
        {
            CreateCircle();
        }
        else if (_shotShape == EShotShape.Triangle)
        {
            CreateTriangle();
        }
        else if (_shotShape == EShotShape.Square)
        {
            CreateSquare();
        }
        else if (_shotShape == EShotShape.XShape)
        {
            CreateXShape();
        }
    }
    private void CreateCircle()
    {
        if (_bulletCount < 3)
        {
            return;
        }

        for (int i = 0; i < _bulletCount; i++)
        {
            float angle = i * 360f / _bulletCount;

            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector2 offset = new Vector2(x, y) * _shapeSize;

            CreateBullet(offset);
        }
    }

    private void CreateTriangle()
    {
        Vector2 top = new Vector2(0f, _shapeSize);
        Vector2 left = new Vector2(-_shapeSize, -_shapeSize);
        Vector2 right = new Vector2(_shapeSize, -_shapeSize);

        int count = _bulletCount / 3;

        CreateLine(top, left, count);
        CreateLine(left, right, count);
        CreateLine(right, top, count);
    }

    private void CreateSquare()
    {
        Vector2 topLeft = new Vector2(-_shapeSize, _shapeSize);
        Vector2 topRight = new Vector2(_shapeSize, _shapeSize);
        Vector2 bottomLeft = new Vector2(-_shapeSize, -_shapeSize);
        Vector2 bottomRight = new Vector2(_shapeSize, -_shapeSize);

        int count = _bulletCount / 4;

        CreateLine(topLeft, topRight, count);
        CreateLine(topRight, bottomRight, count);
        CreateLine(bottomRight, bottomLeft, count);
        CreateLine(bottomLeft, topLeft, count);
    }

    private void CreateXShape()
    {
        Vector2 topLeft = new Vector2(-_shapeSize, _shapeSize);
        Vector2 topRight = new Vector2(_shapeSize, _shapeSize);
        Vector2 bottomLeft = new Vector2(-_shapeSize, -_shapeSize);
        Vector2 bottomRight = new Vector2(_shapeSize, -_shapeSize);

        int count = _bulletCount / 2;

        CreateLine(topLeft, bottomRight, count);
        CreateLine(topRight, bottomLeft, count);
    }

    private void CreateLine(Vector2 start, Vector2 end, int count)
    {
        if (count < 2)
        {
            count = 2;
        }

        for (int i = 0; i < count; i++)
        {
            float t = (float)i / count;

            Vector2 position = Vector2.Lerp(start, end, t);

            CreateBullet(position);
        }
    }

    private void CreateBullet(Vector2 offset)
    {
        Vector2 bulletPosition =
            (Vector2)transform.position + offset;

        CEnemyBullet bullet = CEnemyBulletFactory.Instance.Creat(
            bulletPosition,
            Quaternion.identity
        );

        if (bullet == null)
        {
            return;
        }

        bullet.Fire(_throwDirection, _bulletSpeed);
    }
} 
