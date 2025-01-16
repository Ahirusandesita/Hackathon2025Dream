// --------------------------------------------------------- 
// POWManager.cs 
// 
// CreateDay: 
// Creator  : –ì‘º
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

public class POWManager : MonoBehaviour,IInjectPlayer
{
    [SerializeField]
    private List<POWGroupAsset> POWGroupAssets = default;

    private List<Koma> komas = new List<Koma>();
    public RebellionHandler OnRebellion;

    private void Start()
    {
        
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
            OnRebellion?.Invoke(new RebellionEventArgs(allRebillions.ToArray()), this);
        }
    }

    public void InjectPlayer(PlayerNumber playerNumber)
    {
        
    }
}