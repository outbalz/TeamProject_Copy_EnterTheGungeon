using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class CProjectilePhysicsHandler
{
    // 관통 - 타격시 차감
    public static bool HandlePierce(ref int currentPierceCount)
    {
        if (currentPierceCount > 0)
        {
            currentPierceCount--;
            return true;
        }
        return false;

    }

    // 도탄 - 타격시 차감
    public static bool HandleBounce(ref int currentBounceCount, Rigidbody2D rb, Transform projectileTransform, Vector2 hitDir, Vector2 Normal, float ProjectileSpeed)
    {
   
        if (currentBounceCount <= 0)
        {
            return false;
        }

        currentBounceCount--;

        // 반사 방향 백터 계산
        Vector2 reflectDir = Vector2.Reflect(hitDir, Normal).normalized;
      
        if (rb != null)
        {
            float currentSpeed = rb.velocity.magnitude;

            // 0인상태 방지
            if (currentSpeed <= 0.01f)
            {
                currentSpeed = ProjectileSpeed;
            }

            // 도탄 후 원래 속도 복구
            rb.velocity = reflectDir * currentSpeed;

        }

        // 회전 처리
        if (projectileTransform != null)
        {
            projectileTransform.up = reflectDir;
        }
        return true;
    }

    // 넉뺵
    public static void HandleKnokback(float knockbackPower, Vector2 hitDir, Collider2D targetCollider)
    {
        // 넉백 힘 + 콜라이더 안전장치
        if (knockbackPower < 0 || targetCollider == null)
        {
            return;
        }

        // 타겟 콜라이더 탐색
        Rigidbody2D targetRb = targetCollider.GetComponent<Rigidbody2D>();

        if (targetRb != null)
        {
            // 방향으로 AddForce로 힘줘서 넉백구현
            Vector2 knockbackDir = hitDir.normalized;
            targetRb.AddForce(knockbackDir * knockbackPower, ForceMode2D.Impulse);
            

        }
    }
}
