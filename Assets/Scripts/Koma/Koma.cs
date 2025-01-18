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
    private KomaAsset _nariAsset = default;

    public KomaAsset KomaAsset { get => _komaAsset; set => _komaAsset = value; }
    public KomaAsset NariAsset => _nariAsset;
    public PlayerNumber MyPlayerNumber { get; set; }
    public Vector2Int CurrentPosition { get; set; }
    /// <summary>
    /// 基本的に不変の値。元はどっちの駒？
    /// </summary>
    public PlayerNumber MyAbsolutePlayerNumber { get; set; }
}