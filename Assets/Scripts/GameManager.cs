// --------------------------------------------------------- 
// GameManager.cs 
// 
// CreateDay: 
// Creator  : �쑺�Е�
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