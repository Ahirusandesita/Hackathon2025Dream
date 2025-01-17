// --------------------------------------------------------- 
// KomaAsset.cs 
// 
// CreateDay: 
// Creator  : Takayanagi
// --------------------------------------------------------- 

using UnityEngine;

[CreateAssetMenu(fileName = "NewKomaAsset", menuName = "ScriptableObject/KomaAsset")]
public class KomaAsset : ScriptableObject
{
    [SerializeField]
    private string _komaName = default;
    [SerializeField, Tooltip("‘Š‘ÎÀ•W")]
    private Vector2Int[] _movableDirection = default;
    [SerializeField]
    private Sprite _icon = default;

    public string KomaName => _komaName;
    public Vector2Int[] MovableDirection => _movableDirection;
    public Sprite Icon => _icon;
}