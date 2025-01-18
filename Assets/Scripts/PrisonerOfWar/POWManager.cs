// --------------------------------------------------------- 
// POWManager.cs 
// 
// CreateDay: 
// Creator  : ñÏë∫
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using System;

public class RebellionEventArgs : System.EventArgs
{
    private Koma[] rebellions;
    public IReadOnlyCollection<Koma> Rebellions => rebellions;
    public RebellionEventArgs(Koma[] rebellions)
    {
        this.rebellions = rebellions;
    }
}
public delegate void RebellionHandler(RebellionEventArgs rebellionEventArgs, object sender);
public delegate UniTask WaitWithRebellionHandler(RebellionEventArgs rebellionEventArgs, object sender);
public class POWManager : MonoBehaviour, IInject<PlayerNumber>, IInject<GameManager>, IInject<IReadOnlyList<POWGroupAsset>>
{
    public event RebellionHandler OnRebellion;
    public event WaitWithRebellionHandler OnWaitRebellion;

    public event Action<Koma> OnPOWPut;
    public event Action OnPOWPutEnd;

    private IReadOnlyList<POWGroupAsset> POWGroupAssets;
    private PlayerNumber playerNumber;
    private List<Koma> POWs = new List<Koma>();
    private List<Koma> POWStandBys = new List<Koma>();
    private GameManager gameManager;

    private ClickSystem clickSystem;
    private Ban ban;

    private bool isFirstPowPut = false;
    private void Initialize()
    {
        gameManager.GetComponent<PhaseManager>().OnRebellionCheckStart += (turnEndPlayer) =>
        {
            if (turnEndPlayer == playerNumber)
            {
                Rebellion();
            }
        };

        clickSystem = gameManager.GetComponent<ClickSystem>();
        ban = FindObjectOfType<Ban>();

        gameManager.GetComponent<PhaseManager>().OnPowPutStart += (playerNumber) =>
        {
            if (playerNumber != this.playerNumber)
            {
                return;
            }

            clickSystem.OnClickMasu += POWPut;
            if (POWStandBys.Count == 0)
            {
                gameManager.GetComponent<IPhaseChanger>().POWPutEnd(playerNumber);
                OnPOWPutEnd?.Invoke();
            }
            else
            {
                isFirstPowPut = true;
                OnPOWPut?.Invoke(POWStandBys[0]);
            }

        };
        gameManager.GetComponent<PhaseManager>().OnPowPutEnd += (playerNumber) =>
        {
            if (playerNumber != this.playerNumber)
            {
                return;
            }

            clickSystem.OnClickMasu -= POWPut;

            //âº
            gameManager.GetComponent<IPhaseChanger>().RebellionCheckStart(playerNumber);
        };
    }
    private void POWPut(Masu masu)
    {
        if (isFirstPowPut)
        {
            if (ban.CheckPosition(masu.OwnPosition))
            {
                isFirstPowPut = false;
                gameManager.Me(playerNumber).GetComponent<KomaController>().PutKoma(POWStandBys[0], masu.OwnPosition);
                GetComponent<KomaWaitingArea>().Remove(POWStandBys[0]);
                POWStandBys.RemoveAt(0);

                if (POWStandBys.Count == 0)
                {
                    gameManager.GetComponent<IPhaseChanger>().POWPutEnd(playerNumber);
                    OnPOWPutEnd?.Invoke();
                }
                else
                {
                    OnPOWPut?.Invoke(POWStandBys[0]);
                }
            }
        }
        if (!isFirstPowPut)
        {
            //Ç±Ç±BanÇ™èCê≥Ç≥ÇÍÇÍÇŒåƒÇ‘ä÷êîÇïœÇ¶ÇÈ
            if (ban.CheckPosition(masu.OwnPosition))
            {
                gameManager.Me(playerNumber).GetComponent<KomaController>().PutKoma(POWStandBys[0], masu.OwnPosition);
                GetComponent<KomaWaitingArea>().Remove(POWStandBys[0]);
                POWStandBys.RemoveAt(0);

                if (POWStandBys.Count == 0)
                {
                    gameManager.GetComponent<IPhaseChanger>().POWPutEnd(playerNumber);
                    OnPOWPutEnd?.Invoke();
                }
                else
                {
                    OnPOWPut?.Invoke(POWStandBys[0]);
                }
            }
        }
    }




    public void TurnedIntoPOW(Koma koma)
    {
        koma.GetComponent<PowMesh>().POW.enabled = true;
        koma.GetComponent<PowMesh>().Normal.enabled = false;
        POWs.Add(koma);
        POWStandBys.Add(koma);
    }
    public void CancelPOW(Koma koma)
    {
        koma.GetComponent<PowMesh>().POW.enabled = false;
        koma.GetComponent<PowMesh>().Normal.enabled = true;
        POWs.Remove(koma);
        POWStandBys.Remove(koma);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && playerNumber == PlayerNumber.Player1)
        {
            A().Forget();
        }
        if (Input.GetKeyDown(KeyCode.B) && playerNumber == PlayerNumber.Player2)
        {
            OnWaitRebellion?.Invoke(null, this);
        }
    }
    private async UniTask A()
    {
        if (OnWaitRebellion != null)
        {
            await UniTask.WhenAll(
                OnWaitRebellion?.GetInvocationList()
                    .OfType<WaitWithRebellionHandler>()
                        .Select(async (OnAysncEvent) => await OnAysncEvent.Invoke(null, this)));
        }
    }

    private async void Rebellion()
    {
        List<Koma> allRebillions = new List<Koma>();
        foreach (POWGroupAsset group in POWGroupAssets)
        {
            int count = 0;
            List<Koma> rebillions = new List<Koma>();

            foreach (Koma koma in POWs)
            {
                foreach (KomaAsset groupInKomaAsset in group.KomaAssets)
                {
                    if (groupInKomaAsset == koma.KomaAsset)
                    {
                        rebillions.Add(koma);
                        count++;
                    }
                }
            }

            if (count >= group.NumberOfPOWNeededForRebellion)
            {
                allRebillions.AddRange(rebillions);
            }
            rebillions.Clear();
        }

        if (allRebillions.Count > 0)
        {
            Debug.Log($"{FindObjectOfType<GameManager>().Opponent(playerNumber).GetComponent<PlayerManager>().PlayerNomber}Ç™îΩóêÇãNÇ±ÇµÇ‹ÇµÇΩÅB");
            RebellionEventArgs rebellionEventArgs = new RebellionEventArgs(POWs.ToArray());
            OnRebellion?.Invoke(rebellionEventArgs, this);
            if (OnWaitRebellion != null)
            {
                await UniTask.WhenAll(
                    OnWaitRebellion?.GetInvocationList()
                        .OfType<WaitWithRebellionHandler>()
                            .Select(async (OnAysncEvent) => await OnAysncEvent.Invoke(rebellionEventArgs, this)));
            }
            //POWs.RemoveAll(item => allRebillions.Contains(item));
            POWs.Clear();
        }

        gameManager.GetComponent<IPhaseChanger>().RebellionCheckEnd(playerNumber);
    }
    void IInject<PlayerNumber>.Inject(PlayerNumber playerNumber)
    {
        this.playerNumber = playerNumber;
        Initialize();
    }

    void IInject<GameManager>.Inject(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void IInject<IReadOnlyList<POWGroupAsset>>.Inject(IReadOnlyList<POWGroupAsset> t)
    {
        this.POWGroupAssets = t;
    }
}