// --------------------------------------------------------- 
// KomaController.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KomaController : MonoBehaviour, IInjectPlayer
{
    [SerializeField]
    private InitialKomaPositionAsset _initialKomaPositionAsset = default;
    [SerializeField]
    private Ban _ban = default;
    private List<Koma> _ownKomas = default;
    private PlayerNumber _myPlayerNumber = default;
    private PhaseManager _phaseManager = default;

    public List<Koma> OwnKomas { get => _ownKomas; set => _ownKomas = value; }

    private void Initialize()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        //反乱したとき　演出とかするときは演出開始処理をforeachの中に書く
        gameManager.Opponent(_myPlayerNumber).GetComponent<POWManager>().OnRebellion += (eventData, sender) =>
        {
            foreach (Koma koma in eventData.Rebellions)
            {
                _ownKomas.Add(koma);
            }
        };

        //反乱されたとき　演出とかするときはeventData.Rebillionsを使って演出処理をする
        gameManager.Me(_myPlayerNumber).GetComponent<POWManager>().OnRebellion += (eventData, sender) =>
        {
            _ownKomas.RemoveAll(item => eventData.Rebellions.Contains(item));
        };

        _phaseManager = FindObjectOfType<PhaseManager>();

        ClickSystem clickSystem = FindObjectOfType<ClickSystem>();
        clickSystem.OnClickMasu += masu =>
        {
            switch (_phaseManager.CurrentPhase)
            {
                case PhaseManager.Phase.Attack or PhaseManager.Phase.Move:
                    OnClickKoma(masu.OwnPosition);
                    break;
            }
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