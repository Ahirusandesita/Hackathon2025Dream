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
    [SerializeField, Tooltip("ëäëŒç¿ïW")]
    private List<Vector2Int> _moveablePosition = default;

    public string KomaName => _komaName;
    public IReadOnlyList<Vector2Int> MoveablePosition => _moveablePosition;
}