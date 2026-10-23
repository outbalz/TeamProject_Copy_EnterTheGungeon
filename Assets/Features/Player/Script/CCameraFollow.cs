using UnityEngine;

public class CCameraFollow : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] private Transform _target;
    #endregion

    void Start()
    {
        if (_target == null)
        {
            Debug.LogError("카메라 타겟이 비어있음");
            enabled = false;
            return;
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }
}
