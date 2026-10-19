using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDataBaseManager : CSingletonDontDstroyAtLoad<CDataBaseManager>
{
	#region 인스팩터
	[SerializeField] private CWeaponDatabaseSO _weaponDB;
	[SerializeField] private CPrefabDatabaseSO _prefabDB;
	#endregion

	#region 
	public CWeaponDatabaseSO WeaponDB => _weaponDB;
	public CPrefabDatabaseSO PrefabDB => _prefabDB;
	#endregion

}
