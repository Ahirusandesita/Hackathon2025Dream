// --------------------------------------------------------- 
// Masu.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;

public class Masu : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _ownPosition = default;

    public Vector2Int OwnPosition => _ownPosition;
}