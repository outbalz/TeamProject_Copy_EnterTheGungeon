using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProjectileFactory : CFactory<CProjectileFactory, ProjectileController>
{
    private List<ProjectileController> _bullet = new List<ProjectileController>();
    protected override void PrefabCheck()
    {
        // _prefab 없을 경우 방어코드 
        if (_prefab == null)
        {
            if (CDataBaseManager.Instance.PrefabDB != null)
            {
                if (CDataBaseManager.Instance.PrefabDB.TryGet("Projectile", out CPrefabDataSO PrefabDataSO))
                {

                    if (PrefabDataSO is CProjectilePrefabDataSO dataSO)
                    {
                        _prefab = dataSO.ProjectilePrefab;
                    }
                }
            }
        }
        base.PrefabCheck();
    }

    public override ProjectileController Creat()
    {
        Debug.Log($"현재 풀에 있는 총알 개수: {_bullet.Count}");

        PrefabCheck();

        for (int i = 0; i < _bullet.Count; i++)
        {
            if (_bullet[i] != null && !_bullet[i].gameObject.activeSelf)
            {
                return _bullet[i];
            }
      
        }

        ProjectileController newProjectile = Instantiate(_prefab);
        _bullet.Add(newProjectile);

        return newProjectile;
    }




}
