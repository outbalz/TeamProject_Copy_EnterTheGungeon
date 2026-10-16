using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/GameDataSO/PrefabDataSO/EntityPrefabDataSO", fileName = "EntityPrefabDataSO")]
public class CEntityPrefabDataSO : CPrefabDataSO
{
	#region 인스팩터
	[SerializeField] private CEntityController _prefab;
	#endregion

	#region 프로퍼티
	public CEntityController Prefab => _prefab;
	#endregion
}
