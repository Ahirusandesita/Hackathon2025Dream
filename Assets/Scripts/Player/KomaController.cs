// --------------------------------------------------------- 
// KomaController.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class KomaController : MonoBehaviour, IInjectPlayer
{
    [SerializeField]
    private Ban _ban = default;
    private List<Koma> _ownKomas = default;
    private PlayerNumber _myPlayerNumber = default;

    private void Initialize()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        //���������Ƃ��@���o�Ƃ�����Ƃ��͉��o�J�n������foreach�̒��ɏ���
        gameManager.Opponent(_myPlayerNumber).GetComponent<POWManager>().OnRebellion += (eventData, sender) =>
        {
            foreach (Koma koma in eventData.Rebellions)
            {
                _ownKomas.Add(koma);
            }
        };

        //�������ꂽ�Ƃ��@���o�Ƃ�����Ƃ���eventData.Rebillions���g���ĉ��o����������
        gameManager.Me(_myPlayerNumber).GetComponent<POWManager>().OnRebellion += (eventData, sender) =>
        {
            _ownKomas.RemoveAll(item => eventData.Rebellions.Contains(item));
        };
    }

    public void InjectPlayer(PlayerNumber playerNumber)
    {
        _myPlayerNumber = playerNumber;
        Initialize();
    }

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