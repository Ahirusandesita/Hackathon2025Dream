// --------------------------------------------------------- 
// KomaController.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KomaController : MonoBehaviour
{
    [SerializeField]
    private Ban _ban = default;
    private List<Koma> _ownKomas = default;

    public void OnClickKoma(Vector2Int position)
    {
        int clickedKomaIndex;
        for (clickedKomaIndex = 0; clickedKomaIndex < _ownKomas.Count; clickedKomaIndex++)
        {
            if (_ownKomas[clickedKomaIndex].CurrentPosition == position)
            {
                break;
            }
        }

        Vector2Int[] movablePosition =
            _ban.GetMovablePosition(position, _ownKomas[clickedKomaIndex].KomaAsset.MovableDirection);

    }
}