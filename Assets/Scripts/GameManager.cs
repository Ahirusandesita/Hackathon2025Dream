// --------------------------------------------------------- 
// GameManager.cs 
// 
// CreateDay: 
// Creator  : –ì‘º˜Ð•½
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public interface IInjectPlayer
{
    void InjectPlayer(PlayerNumber playerNumber);
}
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    public GameObject Player1 => player1;
    public GameObject Player2 => player2;

    private void Awake()
    {
        foreach(IInjectPlayer inject in Player1.GetComponentsInChildren<IInjectPlayer>())
        {
            inject.InjectPlayer(Player1.GetComponent<PlayerManager>().PlayerNomber);
        }
        foreach(IInjectPlayer inject in Player2.GetComponentsInChildren<IInjectPlayer>())
        {
            inject.InjectPlayer(Player2.GetComponent<PlayerManager>().PlayerNomber);
        }
    }

    public GameObject Opponent(PlayerNumber playerNumber)
    {
        switch (playerNumber)
        {
            case PlayerNumber.Player1:
                return Player2;
            case PlayerNumber.Player2:
                return Player1;
        }
        throw new System.NullReferenceException();
    }
    public GameObject Me(PlayerNumber playerNumber)
    {
        switch (playerNumber)
        {
            case PlayerNumber.Player1:
                return Player1;
            case PlayerNumber.Player2:
                return Player2;
        }
        throw new System.NullReferenceException();
    }
}