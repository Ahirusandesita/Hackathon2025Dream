// --------------------------------------------------------- 
// Koma.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using System.Linq;
using UnityEngine;

public class Koma : MonoBehaviour
{
    [SerializeField]
    private KomaAsset _komaAsset = default;
    //[SerializeField]
    //private PlayerNomber 
    [SerializeField]
    private Ban _ban = default;
    private Vector2Int _currentPosition = default;

    public KomaAsset KomaAsset => _komaAsset;
    public Vector2Int CurrentPosition => _currentPosition;
}