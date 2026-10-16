using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CProjectilePhysicsHandler : MonoBehaviour
{
    #region 내부 변수
    private CProjectilePhysicsSO _PhysicsData;
    private int _currentPierceCount;
    private int _currentBounceCount;
    private float _knockback;
    #endregion

    // 원본 보존
    public CProjectilePhysicsHandler(CProjectilePhysicsSO physicsData)
    {
        _PhysicsData = physicsData;

        if (_PhysicsData != null)
        {
            _currentPierceCount = _PhysicsData.pierceCount;
            _currentBounceCount = _PhysicsData.BounceCount;
            _knockback = _PhysicsData.Knockback;
        } 
    }



    // 관통 - 타격시 차감
    public bool HandlePierce()
    {
        if (_currentPierceCount > 0)
        {
            _currentPierceCount--;
            return true;
        }
        return false;

    }

    public bool HandleBounce()
    {

        if (_currentBounceCount > 0)
        {
            _currentBounceCount--;

  

        }

        return false;
    }


    public void HandleKnokback()
    {
        if (_knockback <= 0f)
        {
            return;
        }

        


    }
}
