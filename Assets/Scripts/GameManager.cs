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
using System.Collections.Generic;
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
    [SerializeField]
    private List<POWGroupAsset> POWGroupAssets = new List<POWGroupAsset>();

    [SerializeField]
    private List<GameObject> player1_komaWaitingAreas;
    [SerializeField]
    private List<GameObject> player2_komaWaitingAreas;

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
        foreach (IInject<IReadOnlyList<POWGroupAsset>> item in InterfaceUtils.FindObjectOfInterfaces<IInject<IReadOnlyList<POWGroupAsset>>>())
        {
            item.Inject(POWGroupAssets);
        }


        foreach (IInjectPlayer inject in Player1.GetComponentsInChildren<IInjectPlayer>())
        {
            inject.InjectPlayer(Player1.GetComponent<PlayerManager>().PlayerNumber);
        }
        foreach (IInjectPlayer inject in Player2.GetComponentsInChildren<IInjectPlayer>())
        {
            inject.InjectPlayer(Player2.GetComponent<PlayerManager>().PlayerNumber);
        }

        foreach (IInject<PlayerNumber> item in Player1.GetComponentsInChildren<IInject<PlayerNumber>>())
        {
            item.Inject(Player1.GetComponent<PlayerManager>().PlayerNumber);
        }
        foreach (IInject<PlayerNumber> item in Player2.GetComponentsInChildren<IInject<PlayerNumber>>())
        {
            item.Inject(Player2.GetComponent<PlayerManager>().PlayerNumber);
        }

        Player1.GetComponent<KomaWaitingArea>().InjectWaitingTransform(player1_komaWaitingAreas);
        Player2.GetComponent<KomaWaitingArea>().InjectWaitingTransform(player2_komaWaitingAreas);

    }

    private async void Start()
    {
        await UniTask.WaitForSeconds(1f);
        GameStart().Forget();

        //GetComponent<TurnManager>().OnTurnEnd += (playerNumber) =>
        //{
        //    IPhaseChanger phaseChanger = GetComponent<IPhaseChanger>();
        //    switch (playerNumber)
        //    {
        //        case PlayerNumber.Player1:
        //            phaseChanger.AttackStart(PlayerNumber.Player2);
        //            break;
        //        case PlayerNumber.Player2:
        //            phaseChanger.AttackStart(PlayerNumber.Player1);
        //            break;
        //    }
        //};
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

    public async void GameEnd(PlayerNumber winner)
    {
        // 勝敗者設定
        if (winner == PlayerNumber.Player1)
        {
            //Debug
            SceneChanger.Get().GoResult1Scene();
        }
        else
        {
            //Debug
            SceneChanger.Get().GoResult2Scene();
        }
        await UniTask.CompletedTask;
    }
}