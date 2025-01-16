// --------------------------------------------------------- 
// TurnManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System;

public class TurnManager : MonoBehaviour
{
    public event Action<PlayerNumber> OnTurnEnd;
    private PlayerNumber nowTurnPlayerNumber = PlayerNumber.Player1;
    public PlayerNumber NowTurnPlayerNumber => nowTurnPlayerNumber;
    public void TurnEnd(PlayerNumber playerNumber)
    {
        OnTurnEnd?.Invoke(playerNumber);
        switch (playerNumber)
        {
            case PlayerNumber.Player1:
                nowTurnPlayerNumber = PlayerNumber.Player2;
                break;
            case PlayerNumber.Player2:
                nowTurnPlayerNumber = PlayerNumber.Player1;
                break;
        }
    }
}