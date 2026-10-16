using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    #region 
    private CProjectileDataSO _ProjectileData;
    private float _speed;
    private Coroutine _lifeTimerCorotine;
    #endregion

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
        _ProjectileData = data;
        _speed = _ProjectileData.ProjectileSpeed;

        // 수명 (라이프 타임)값 가져오기
        float lifeTime = _ProjectileData.ProjectilePhysics.LifeTime;
        _lifeTimerCorotine = StartCoroutine(LifeTimer(lifeTime));
  

    }

    private void Update()
    {
        // 마우스 방향으로 발사
        transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
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
    }

}
