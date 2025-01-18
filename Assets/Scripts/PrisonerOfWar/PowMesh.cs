// --------------------------------------------------------- 
// PowMesh.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;

public class PowMesh : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer normal;
    [SerializeField]
    private MeshRenderer pow;
    public MeshRenderer Normal => normal;
    public MeshRenderer POW => pow;
}