// --------------------------------------------------------- 
// KomaAsset.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKomaAsset", menuName = "ScriptableObject/KomaAsset")]
public class KomaAsset : ScriptableObject
{
    [SerializeField]
    private string _komaName = default;
    [SerializeField, Tooltip("‘Š‘ÎÀ•W")]
    private Vector2Int[] _movableDirection = default;

    public string KomaName => _komaName;
    public Vector2Int[] MovableDirection => _movableDirection;
}