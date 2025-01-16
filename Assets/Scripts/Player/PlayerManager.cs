// --------------------------------------------------------- 
// PlayerManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerNumber _playerNumber = default;
    public PlayerNumber PlayerNomber => _playerNumber;

    public static int GetMoveDirectionCoefficient(PlayerNumber playerNumber)
    {
        switch (playerNumber)
        {
            case PlayerNumber.Player1:
                return -1;
            case PlayerNumber.Player2:
                return 1;
            default:
                throw new System.ArgumentOutOfRangeException();
        }
    }
}