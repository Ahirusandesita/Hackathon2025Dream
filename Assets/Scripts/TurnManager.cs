// --------------------------------------------------------- 
// TurnManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System;

public class TurnManager : MonoBehaviour,IInject<GameManager>
{
    /// <summary>
    /// PlayerNumber : ターン終わった人のプレイヤーのナンバー
    /// </summary>
    public event Action<PlayerNumber> OnTurnEnd;
    private PlayerNumber nowTurnPlayerNumber = PlayerNumber.Player1;
    private GameManager gameManager;

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

    void IInject<GameManager>.Inject(GameManager gameManager)
    {
        this.gameManager = gameManager;

        //テスト

        gameManager.GetComponent<PhaseManager>().OnRebellionCheckEnd += (playerNumber) =>
        {
            TurnEnd(playerNumber);

            gameManager.GetComponent<IPhaseChanger>().AttackStart(nowTurnPlayerNumber);
        };
    }
}