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
    //[SerializeField]
    //private 
    [SerializeField]
    private Ban _ban = default;

    public KomaAsset KomaAsset => _komaAsset;

    public void Move()
    {
        //_ban.GetMovablePosition()
    }
}