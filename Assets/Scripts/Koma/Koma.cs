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
    [SerializeField]
    private Ban _ban = default;
    private Vector2Int _currentPosition = default;
    private PlayerNumber _myPlayerNumber = default;

    public KomaAsset KomaAsset => _komaAsset;
    public Vector2Int CurrentPosition => _currentPosition;
    public PlayerNumber MyPlayerNumber => _myPlayerNumber;
    /// <summary>
    /// 基本的に不変の値。元はどっちの駒？
    /// </summary>
    public PlayerNumber MyAbsolutePlayerNumber { get; set; }
}