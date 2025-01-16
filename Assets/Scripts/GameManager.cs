// --------------------------------------------------------- 
// GameManager.cs 
// 
// CreateDay: 
// Creator  : –ì‘º˜Ğ•½
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    public GameObject Player1 => player1;
    public GameObject Player2 => player2;
}