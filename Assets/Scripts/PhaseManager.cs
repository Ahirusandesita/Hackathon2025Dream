// --------------------------------------------------------- 
// PhaseManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using System;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public enum Phase
    {
        Attack,
        Move,
        KingMove,
        EndCheck,
    }

    private Phase _currentPhase = default;

    public Phase CurrentPhase => _currentPhase;

    public event Action<PlayerNumber> OnAttackStart = default;
    public event Action<PlayerNumber> OnAttackEnd = default;
    public event Action<PlayerNumber> OnMoveStart = default;
    public event Action<PlayerNumber> OnMoveEnd = default;


    public void AttackStart(PlayerNumber playerNumber)
    {
        OnAttackStart?.Invoke(playerNumber);
    }

    public void AttackEnd(PlayerNumber playerNumber)
    {
        OnAttackEnd?.Invoke(playerNumber);
    }

    public void MoveStart(PlayerNumber playerNumber)
    {
        OnMoveStart?.Invoke(playerNumber);
    }

    public void MoveEnd(PlayerNumber playerNumber)
    {
        OnMoveEnd?.Invoke(playerNumber);
    }
}