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


    private IReadOnlyList<POWGroupAsset> POWGroupAssets;
    private PlayerNumber playerNumber;
    private List<Koma> POWs = new List<Koma>();
    private List<Koma> POWStandBys = new List<Koma>();
    private GameManager gameManager;

    private ClickSystem clickSystem;
    private Ban ban;

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
            clickSystem.OnClickMasu += POWPut;
        };
        gameManager.GetComponent<PhaseManager>().OnPowPutEnd += (playerNumber) =>
        {
            clickSystem.OnClickMasu -= POWPut;

            //âº
            gameManager.GetComponent<IPhaseChanger>().RebellionCheckStart(playerNumber);
        };
    }
    private void POWPut(Masu masu)
    {
        if (ban.CheckPosition(masu.OwnPosition))
        {
            gameManager.Me(playerNumber).GetComponent<KomaController>().PutKoma(POWStandBys[0], masu.OwnPosition);
            POWStandBys.RemoveAt(0);

            if (POWStandBys.Count == 0)
            {
                gameManager.GetComponent<IPhaseChanger>().POWPutEnd(playerNumber);
            }
        }
    }

    public void TurnedIntoPOW(Koma koma)
    {
        POWs.Add(koma);

        POWStandBys.Add(koma);
    }
    public void CancelPOW(Koma koma)
    {
        POWs.Remove(koma);
        POWStandBys.Remove(koma);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && playerNumber == PlayerNumber.Player1)
        {
            OnWaitRebellion?.Invoke(null, this);
        }
        if (Input.GetKeyDown(KeyCode.B) && playerNumber == PlayerNumber.Player2)
        {
            OnWaitRebellion?.Invoke(null, this);
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
            OnRebellion?.Invoke(new RebellionEventArgs(allRebillions.ToArray()), this);
            if (OnWaitRebellion != null)
            {
                await UniTask.WhenAll(
                    OnWaitRebellion?.GetInvocationList()
                        .OfType<WaitWithHandler>()
                            .Select(async (OnAysncEvent) => await OnAysncEvent.Invoke()));
            }
            POWs.RemoveAll(item => allRebillions.Contains(item));
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