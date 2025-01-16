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
    public void TurnEnd(PlayerNumber playerNumber)
    {
        OnTurnEnd?.Invoke(playerNumber);
    }
}