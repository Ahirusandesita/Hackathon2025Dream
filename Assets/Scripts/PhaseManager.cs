// --------------------------------------------------------- 
// PhaseManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using System;
using UnityEngine;

public interface IPhaseChanger
{
    void AttackStart(PlayerNumber playerNumber);
    void AttackEnd(PlayerNumber playerNumber);
    void MoveStart(PlayerNumber playerNumber);
    void MoveEnd(PlayerNumber playerNumber);
    void KingMoveStart(PlayerNumber playerNumber);
    void KingMoveEnd(PlayerNumber playerNumber);
    void POWPutStart(PlayerNumber playerNumber);
    void POWPutEnd(PlayerNumber playerNumber);
    void RebellionCheckStart(PlayerNumber playerNumber);
    void RebellionCheckEnd(PlayerNumber playerNumber);
}

public class PhaseManager : MonoBehaviour, IPhaseChanger
{
    public enum Phase
    {
        Attack,
        Move,
        KingMove,
        PowPut,
        RebellionCheck,
    }

    private Phase _currentPhase = default;

    public Phase CurrentPhase => _currentPhase;

    public event Action<PlayerNumber> OnAttackStart = default;
    public event Action<PlayerNumber> OnAttackEnd = default;
    public event Action<PlayerNumber> OnMoveStart = default;
    public event Action<PlayerNumber> OnMoveEnd = default;
    public event Action<PlayerNumber> OnKingMoveStart = default;
    public event Action<PlayerNumber> OnKingMoveEnd = default;
    public event Action<PlayerNumber> OnPowPutStart = default;
    public event Action<PlayerNumber> OnPowPutEnd = default;
    public event Action<PlayerNumber> OnRebellionCheckStart = default;
    public event Action<PlayerNumber> OnRebellionCheckEnd = default;

    void IPhaseChanger.AttackStart(PlayerNumber playerNumber)
    {

        if (!(_currentPhase == Phase.Attack || _currentPhase == Phase.Move || _currentPhase == Phase.RebellionCheck))
        {
            return;
        }
        Debug.Log("Next phase start");
        _currentPhase = Phase.Attack;
        OnAttackStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.AttackEnd(PlayerNumber playerNumber)
    {
        if (_currentPhase != Phase.Attack)
        {
            return;
        }
        OnAttackEnd?.Invoke(playerNumber);
        // 攻撃終了時、自動的にMoveフェーズに遷移する
        (this as IPhaseChanger).MoveStart(playerNumber);
    }

    void IPhaseChanger.MoveStart(PlayerNumber playerNumber)
    {
        //アタックの時にMoveフェーズに以降する
        if (!(_currentPhase == Phase.Attack || _currentPhase == Phase.Move))
        {
            return;
        }
        _currentPhase = Phase.Move;
        OnMoveStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.MoveEnd(PlayerNumber playerNumber)
    {
        if (_currentPhase != Phase.Move)
        {
            return;
        }

        OnMoveEnd?.Invoke(playerNumber);
        (this as IPhaseChanger).KingMoveStart(playerNumber);
    }

    void IPhaseChanger.KingMoveStart(PlayerNumber playerNumber)
    {
        //Moveの時にキングムーブフェーズに以降する
        if (_currentPhase != Phase.Move)
        {
            return;
        }
        _currentPhase = Phase.KingMove;
        OnKingMoveStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.KingMoveEnd(PlayerNumber playerNumber)
    {
        if (_currentPhase != Phase.KingMove)
        {
            return;
        }
        OnKingMoveEnd?.Invoke(playerNumber);
        (this as IPhaseChanger).POWPutStart(playerNumber);
    }

    void IPhaseChanger.RebellionCheckStart(PlayerNumber playerNumber)
    {
        //Pow配置の時に反乱フェーズに以降する
        if (_currentPhase != Phase.PowPut)
        {
            return;
        }
        Debug.Log("RebellionCheck phase start");
        _currentPhase = Phase.RebellionCheck;
        OnRebellionCheckStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.RebellionCheckEnd(PlayerNumber playerNumber)
    {
        if (_currentPhase != Phase.RebellionCheck)
        {
            return;
        }
        Debug.Log("RebellionCheck phase end");
        OnRebellionCheckEnd?.Invoke(playerNumber);
    }

    void IPhaseChanger.POWPutStart(PlayerNumber playerNumber)
    {
        //KingMoveの時にPOW配置フェーズに以降する
        if (_currentPhase != Phase.KingMove)
        {
            return;
        }
        Debug.Log("POWPUT phase start");
        _currentPhase = Phase.PowPut;
        OnPowPutStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.POWPutEnd(PlayerNumber playerNumber)
    {
        if (_currentPhase != Phase.PowPut)
        {
            return;
        }
        Debug.Log("POWPUT phase end");
        OnPowPutEnd?.Invoke(playerNumber);
    }
}