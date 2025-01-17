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
    public event Action<PlayerNumber> OnRebellionCheckStart = default;
    public event Action<PlayerNumber> OnRebellionCheckEnd = default;


    void IPhaseChanger.AttackStart(PlayerNumber playerNumber)
    {
        _currentPhase = Phase.Attack;
        OnAttackStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.AttackEnd(PlayerNumber playerNumber)
    {
        OnAttackEnd?.Invoke(playerNumber);
    }

    void IPhaseChanger.MoveStart(PlayerNumber playerNumber)
    {
        _currentPhase = Phase.Move;
        OnMoveStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.MoveEnd(PlayerNumber playerNumber)
    {
        OnMoveEnd?.Invoke(playerNumber);
    }

    void IPhaseChanger.KingMoveStart(PlayerNumber playerNumber)
	{
        _currentPhase = Phase.KingMove;
        OnKingMoveStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.KingMoveEnd(PlayerNumber playerNumber)
	{
        OnKingMoveEnd?.Invoke(playerNumber);
	}

    void IPhaseChanger.RebellionCheckStart(PlayerNumber playerNumber)
	{
        _currentPhase = Phase.RebellionCheck;
        OnRebellionCheckStart?.Invoke(playerNumber);
	}

    void IPhaseChanger.RebellionCheckEnd(PlayerNumber playerNumber)
	{
        OnRebellionCheckEnd?.Invoke(playerNumber);
	}
}