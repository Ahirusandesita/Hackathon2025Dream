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
}