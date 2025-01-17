// --------------------------------------------------------- 
// Koma.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;

public class Koma : MonoBehaviour
{
    [SerializeField]
    private KomaAsset _komaAsset = default;
    private PlayerNumber _myPlayerNumber = default;

    public KomaAsset KomaAsset => _komaAsset;
    public PlayerNumber MyPlayerNumber => _myPlayerNumber;
    public Vector2Int CurrentPosition { get; set; }
    /// <summary>
    /// 基本的に不変の値。元はどっちの駒？
    /// </summary>
    public PlayerNumber MyAbsolutePlayerNumber { get; set; }
}