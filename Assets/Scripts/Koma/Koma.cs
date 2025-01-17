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

    public KomaAsset KomaAsset => _komaAsset;
    public PlayerNumber MyPlayerNumber { get; set; }
    public Vector2Int CurrentPosition { get; set; }
    /// <summary>
    /// 基本的に不変の値。元はどっちの駒？
    /// </summary>
    public PlayerNumber MyAbsolutePlayerNumber { get; set; }
}