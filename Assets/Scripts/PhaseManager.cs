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
        if (!(_currentPhase == Phase.Attack || _currentPhase == Phase.RebellionCheck))
        {
            return;
        }
        _currentPhase = Phase.Attack;
        OnAttackStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.AttackEnd(PlayerNumber playerNumber)
    {
        OnAttackEnd?.Invoke(playerNumber);
    }

    void IPhaseChanger.MoveStart(PlayerNumber playerNumber)
    {
        //�A�^�b�N�̎���Move�t�F�[�Y�Ɉȍ~����
        if (_currentPhase != Phase.Attack)
        {
            return;
        }
        _currentPhase = Phase.Move;
        OnMoveStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.MoveEnd(PlayerNumber playerNumber)
    {
        OnMoveEnd?.Invoke(playerNumber);
    }

    void IPhaseChanger.KingMoveStart(PlayerNumber playerNumber)
    {
        //Move�̎��ɃL���O���[�u�t�F�[�Y�Ɉȍ~����
        if (_currentPhase != Phase.Move)
        {
            return;
        }
        _currentPhase = Phase.KingMove;
        OnKingMoveStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.KingMoveEnd(PlayerNumber playerNumber)
    {
        OnKingMoveEnd?.Invoke(playerNumber);
    }

    void IPhaseChanger.RebellionCheckStart(PlayerNumber playerNumber)
    {
        //Pow�z�u�̎��ɔ����t�F�[�Y�Ɉȍ~����
        if (_currentPhase != Phase.PowPut)
        {
            return;
        }
        _currentPhase = Phase.RebellionCheck;
        OnRebellionCheckStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.RebellionCheckEnd(PlayerNumber playerNumber)
    {
        OnRebellionCheckEnd?.Invoke(playerNumber);
    }

    void IPhaseChanger.POWPutStart(PlayerNumber playerNumber)
    {
        //KingMove�̎���POW�z�u�t�F�[�Y�Ɉȍ~����
        if (_currentPhase != Phase.KingMove)
        {
            return;
        }
        _currentPhase = Phase.PowPut;
        OnPowPutStart?.Invoke(playerNumber);
    }

    void IPhaseChanger.POWPutEnd(PlayerNumber playerNumber)
    {
        OnPowPutEnd?.Invoke(playerNumber);
    }
}