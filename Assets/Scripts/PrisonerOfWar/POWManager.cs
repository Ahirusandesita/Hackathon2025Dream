// --------------------------------------------------------- 
// POWManager.cs 
// 
// CreateDay: 
// Creator  : –ì‘º
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

public class POWManager : MonoBehaviour, IInject<PlayerNumber>, IInject<GameManager>
{
    [SerializeField]
    private List<POWGroupAsset> POWGroupAssets = default;
    private PlayerNumber playerNumber;
    private List<Koma> komas = new List<Koma>();
    private GameManager gameManager;
    public event RebellionHandler OnRebellion;
    private void Initialize()
    {
        gameManager.GetComponent<TurnManager>().OnTurnEnd += (turnEndPlayer) =>
        {
            if (turnEndPlayer == playerNumber)
            {
                Rebellion();
            }
        };
    }

    public void TurnedIntoPOW(Koma koma)
    {
        komas.Add(koma);
    }

    private void Rebellion()
    {
        List<Koma> allRebillions = new List<Koma>();
        foreach (POWGroupAsset group in POWGroupAssets)
        {
            int count = 0;
            List<Koma> rebillions = new List<Koma>();

            foreach (Koma koma in komas)
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
            Debug.Log($"{FindObjectOfType<GameManager>().Opponent(playerNumber).GetComponent<PlayerManager>().PlayerNomber}‚ª”½—‚ð‹N‚±‚µ‚Ü‚µ‚½B");
            OnRebellion?.Invoke(new RebellionEventArgs(allRebillions.ToArray()), this);
            komas.RemoveAll(item => allRebillions.Contains(item));
        }
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
}