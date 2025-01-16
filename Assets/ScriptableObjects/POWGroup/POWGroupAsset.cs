// --------------------------------------------------------- 
// POWGroupAsset.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "NewPOWGroupAsset", menuName = "ScriptableObject/POWGroupAsset")]
public class POWGroupAsset : ScriptableObject
{
    [SerializeField]
    private int numberofPOWNeededForRebellion = 4;
    [SerializeField]
    private List<KomaAsset> komaAssets = new List<KomaAsset>();
    public IReadOnlyList<KomaAsset> KomaAssets => komaAssets;
    public int NumberOfPOWNeededForRebellion => numberofPOWNeededForRebellion;
}