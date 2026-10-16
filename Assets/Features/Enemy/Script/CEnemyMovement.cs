using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMovement : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float _moveSpeed = 2f;


    public void MoveToTarget(Transform target)
    {
        if (target == null)
        {
            return;
        }

        Vector2 direction = target.position - transform.position;

        direction.Normalize();

        transform.position += (Vector3)(direction * _moveSpeed * Time.deltaTime);
    }
}
