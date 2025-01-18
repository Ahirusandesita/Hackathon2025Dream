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
    [SerializeField, Tooltip("���΍��W")]
    private Vector2Int[] _movableDirection = default;
    [SerializeField, Tooltip("�Փ˔�����s��Direction�i���K���j")]
    private Vector2Int[] _collidableDirection = default;
    [SerializeField]
    private Sprite _icon = default;
    [SerializeField]
    private bool _canNari = default;

    public string KomaName => _komaName;
    public Vector2Int[] MovableDirection => _movableDirection;
    public Vector2Int[] CollidableDirection => _collidableDirection;
    public Sprite Icon => _icon;
    public bool CanNari => _canNari;
}