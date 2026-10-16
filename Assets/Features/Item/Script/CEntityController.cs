using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEntityController : MonoBehaviour
{
	#region 인스팩터
	[SerializeField] private Rigidbody2D _rb;
	#endregion

	#region 프로퍼티
	public Rigidbody2D Rb => _rb;
    #endregion

    public void InitializeEntitiy(CEntityDataSO data)
    {
        
    }
}
