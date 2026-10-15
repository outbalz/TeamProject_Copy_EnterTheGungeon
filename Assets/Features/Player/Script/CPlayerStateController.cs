using UnityEngine;

public enum EPlayerState
{
    Idle,
    Move,
    Roll,
    Hit,
    Dead
}

public class CPlayerStateController : MonoBehaviour
{
    #region 내부 변수
    [SerializeField] private EPlayerState _currentState = EPlayerState.Idle;
    #endregion

    #region 프로퍼티
    public EPlayerState CurrentState => _currentState;
    #endregion

    // 플레이어 상태 변경
    public void ChangeState(EPlayerState state)
    {
        _currentState = state;
    }
}
