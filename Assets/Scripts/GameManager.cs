// --------------------------------------------------------- 
// GameManager.cs 
// 
// CreateDay: 
// Creator  : 野村侑平
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;
using System.Linq;
using System;
/// <summary>
/// プレイヤーナンバーがInjectされる
/// </summary>
public interface IInjectPlayer
{
    void InjectPlayer(PlayerNumber playerNumber);
}
public interface IInject<T>
{
    void Inject(T t);
}
public delegate UniTask WaitWithHandler();
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    public GameObject Player1 => player1;
    public GameObject Player2 => player2;

    public event WaitWithHandler OnWaitGameStart;
    public event Action OnGameStart;

    private void Awake()
    {
        foreach (IInject<GameManager> item in InterfaceUtils.FindObjectOfInterfaces<IInject<GameManager>>())
        {
            item.Inject(this);
        }


        foreach (IInjectPlayer inject in Player1.GetComponentsInChildren<IInjectPlayer>())
        {
            inject.InjectPlayer(Player1.GetComponent<PlayerManager>().PlayerNomber);
        }
        foreach (IInjectPlayer inject in Player2.GetComponentsInChildren<IInjectPlayer>())
        {
            inject.InjectPlayer(Player2.GetComponent<PlayerManager>().PlayerNomber);
        }

        foreach (IInject<PlayerNumber> item in Player1.GetComponentsInChildren<IInject<PlayerNumber>>())
        {
            item.Inject(Player1.GetComponent<PlayerManager>().PlayerNomber);
        }
        foreach (IInject<PlayerNumber> item in Player2.GetComponentsInChildren<IInject<PlayerNumber>>())
        {
            item.Inject(Player2.GetComponent<PlayerManager>().PlayerNomber);
        }

        GameStart().Forget();
    }
    /// <summary>
    /// 対戦相手のPlayerGameObjectを取得する
    /// </summary>
    /// <param name="playerNumber"></param>
    /// <returns></returns>
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
    /// <summary>
    /// 自分のPlayerGameObjectを取得する
    /// </summary>
    /// <param name="playerNumber"></param>
    /// <returns></returns>
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

    private async UniTaskVoid GameStart()
    {
        OnGameStart?.Invoke();

        if (OnWaitGameStart != null)
        {
            await UniTask.WhenAll(
                OnWaitGameStart?.GetInvocationList()
                    .OfType<WaitWithHandler>()
                        .Select(async (OnAysncEvent) => await OnAysncEvent.Invoke()));
        }

        GetComponent<IPhaseChanger>().AttackStart(PlayerNumber.Player1);
    }
}