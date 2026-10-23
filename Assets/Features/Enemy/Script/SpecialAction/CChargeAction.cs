using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChargeAction : MonoBehaviour
{
    [Header("돌진 설정")]
    [SerializeField] private float _chargeSpeed = 8f;
    [SerializeField] private float _chargeTime = 0.5f;
    [SerializeField] private float _prepareTime = 1f;

    [Header("공격 설정")]
    [SerializeField] private int _damage = 1;

    private Rigidbody2D _rigidbody2D;
    private Transform _player;

    private bool _isCharging = false;
    private bool _isPreparing = false;
    private Vector2 _chargeDirection;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void StartCharge(Transform player)
    {
        if (_isCharging || _isPreparing || player == null)
        {
            return;
        }

        _player = player;
        StartCoroutine(CoCharge());
    }

    private IEnumerator CoCharge()
    {
        _isPreparing = true;

        _chargeDirection = (_player.position - transform.position).normalized;

        yield return new WaitForSeconds(_prepareTime);

        _isPreparing = false;
        _isCharging = true;

        _rigidbody2D.velocity = _chargeDirection * _chargeSpeed;

        yield return new WaitForSeconds(_chargeTime);

        _rigidbody2D.velocity = Vector2.zero;
        _isCharging = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isCharging)
        {
            return;
        }

        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log(_damage);
        }
    }
}
