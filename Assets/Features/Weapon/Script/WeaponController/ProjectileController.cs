using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    #region 레이어
    [SerializeField] private LayerMask wall;
    private int LayerEnemy => LayerMask.NameToLayer("Enemy");
    #endregion

    #region 내부 변수
    private CProjectileDataSO _projectileData; // 투사체 데이터
    private Coroutine _lifeTimerCorotine; // 수명 코루틴
    private Rigidbody2D _rb;

    private int _currentPierceCount;
    private int _currentBounceCount;
    private HashSet<Collider2D> _hitTarget = new HashSet<Collider2D>();
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetUp(CProjectileDataSO data)
    {
        // 유효성 검사
        if (data == null || data.ProjectilePhysics == null)
        {
            return;
        }

        // 수명 코루틴(타이머) 돌고있으면 중지
        if (_lifeTimerCorotine != null)
        {
            StopCoroutine(_lifeTimerCorotine);
        }

        _projectileData = data;
        var physics = _projectileData.ProjectilePhysics;

        _currentPierceCount = physics.PierceCount;
        _currentBounceCount = physics.BounceCount;

        // HashSet<Collider2D>초기화
        _hitTarget.Clear();

        // 속도, 방향 초기화
        if (_rb != null)
        {
            Vector2 moveDir = transform.up;
            _rb.velocity = moveDir * _projectileData.ProjectileSpeed;
        }

        // 수명 코루틴 실행
        _lifeTimerCorotine = StartCoroutine(LifeTimer(_projectileData.LifeTime));
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 투사체 데이터 없을경우 방어코드
        if (_projectileData == null || _projectileData.ProjectilePhysics == null)
        {
            return;
        }

        int hitlayer = collision.gameObject.layer; // 맞은 콜라이더 판정

        // 검사 (충돌 오브젝트 검사)
        if (hitlayer == LayerEnemy)
        {
            Collider2D targetCollider = collision.collider;

            // 중복 방지
            if (!_hitTarget.Contains(targetCollider))
            {
                _hitTarget.Add(targetCollider);
                _projectileData.OnHit().Forget();

                Vector2 hitDir;

                if (_rb != null && _rb.velocity != Vector2.zero)
                {
                    hitDir = _rb.velocity.normalized;
                }

                else
                {
                    hitDir = transform.up;
                }

                // 관통계산처리 (핸들러한테)
                bool isPireced = CProjectilePhysicsHandler.HandlePierce(ref _currentPierceCount);

                // 넉백계산처리 (핸들러한테)
                CProjectilePhysicsHandler.HandleKnokback(_projectileData.ProjectilePhysics.Knockback, hitDir, targetCollider);
       
                if (!isPireced)
                {
                    ReturnToPool();
                }
            }
        }

       
    }


    private IEnumerator LifeTimer(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        ReturnToPool();

    }

    private void ReturnToPool()
    {
        // 수명 초기화
        _lifeTimerCorotine = null;
        // 비활성화
        gameObject.SetActive(false);

        Debug.Log("총알 반환"); 
    
    }

}
