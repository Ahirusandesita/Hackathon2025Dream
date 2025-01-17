// --------------------------------------------------------- 
// KomaController.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class KomaController : MonoBehaviour, IInjectPlayer
{
    [SerializeField]
    private InitialKomaPositionAsset _initialKomaPositionAsset = default;
    [SerializeField]
    private Ban _ban = default;
    [SerializeField]
    private BanUI _banUI = default;
    private List<Koma> _ownKomas = default;
    private PlayerNumber _myPlayerNumber = default;
    private PhaseManager _phaseManager = default;
    private GameManager _gameManager = default;

    public List<Koma> OwnKomas { get => _ownKomas; set => _ownKomas = value; }

    private void Initialize()
    {
        _gameManager = FindObjectOfType<GameManager>();
        // �΋ǊJ�n�C�x���g���w�ǂ��A�������z�u
        _gameManager.OnWaitGameStart += SetInitializeKomas;

        //���������Ƃ��@���o�Ƃ�����Ƃ��͉��o�J�n������foreach�̒��ɏ���
        _gameManager.Opponent(_myPlayerNumber).GetComponent<POWManager>().OnRebellion += (eventData, sender) =>
        {
            foreach (Koma koma in eventData.Rebellions)
            {
                _ownKomas.Add(koma);
            }
        };

        //�������ꂽ�Ƃ��@���o�Ƃ�����Ƃ���eventData.Rebillions���g���ĉ��o����������
        _gameManager.Me(_myPlayerNumber).GetComponent<POWManager>().OnRebellion += (eventData, sender) =>
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
        //int clickedKomaIndex;
        //for (clickedKomaIndex = 0; clickedKomaIndex < _ownKomas.Count; clickedKomaIndex++)
        //{
        //    if (_ownKomas[clickedKomaIndex].CurrentPosition == position)
        //    {
        //        break;
        //    }
        //}

        //if (clickedKomaIndex == _ownKomas.Count)
        //{
        //    return;
        //}

        //Vector2Int[] movablePosition =
        //    _ban.GetMovablePosition(position, _ownKomas[clickedKomaIndex].KomaAsset.MovableDirection);

    }

    public bool IsExistOwnKomaAtPosition(Vector2Int position)
    {
        foreach (var koma in _ownKomas)
        {
            if (position == koma.CurrentPosition)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsExistOwnKomaAtPosition(Vector2Int position, out Vector2Int[] movablePositions)
    {
        foreach (var koma in _ownKomas)
        {
            if (position == koma.CurrentPosition)
            {
                movablePositions = koma.KomaAsset.MovableDirection;
                return true;
            }
        }

        movablePositions = null;
        return false;
    }

    public void TakeAttack(Vector2Int takeAttackPosition)
    {
        int i;
        for (i = 0; i < _ownKomas.Count; i++)
        {
            if (_ownKomas[i].CurrentPosition == takeAttackPosition)
            {
                break;
            }
        }

        // �������ꂽ��ߗ���������A�ߗ��ɂ��Ȃ�
        if (_ownKomas[i].MyAbsolutePlayerNumber != _myPlayerNumber)
        {
            _gameManager.Me(_myPlayerNumber).GetComponent<POWManager>().CancelPOW(_ownKomas[i]);
            _gameManager.Opponent(_myPlayerNumber).GetComponent<POWManager>().TurnedIntoPOW(_ownKomas[i]);
        }
        // ����ɋ��n��
        _gameManager.Opponent(_myPlayerNumber).GetComponent<KomaController>().OwnKomas.Add(_ownKomas[i]);
        _ownKomas.RemoveAt(i);
        _ban.RemoveKoma(takeAttackPosition);
    }

    private async UniTask SetInitializeKomas()
    {
        _ownKomas = new List<Koma>();
        Vector2Int reversePosition = new Vector2Int(8, 8);

        for (int i = 0; i < _initialKomaPositionAsset.InitialPositions.Count; i++)
        {
            Vector2Int masuPosition;
            if (_myPlayerNumber == PlayerNumber.Player1)
            {
                masuPosition = _initialKomaPositionAsset.InitialPositions[i].Position;
            }
            else
            {
                masuPosition = reversePosition - _initialKomaPositionAsset.InitialPositions[i].Position;
            }

            Vector3 worldPosition = _banUI.GetWorldPosition(masuPosition);
            Koma koma = Instantiate(
                _initialKomaPositionAsset.InitialPositions[i].Koma,
                worldPosition,
                Quaternion.identity
                );
            _ownKomas.Add(koma);
            _ownKomas[i].CurrentPosition = masuPosition;
            _ownKomas[i].MyPlayerNumber = _myPlayerNumber;
            _ownKomas[i].MyAbsolutePlayerNumber = _myPlayerNumber;
            _ban.SetKoma(koma, masuPosition);
        }

        // Debug
        await UniTask.CompletedTask;
    }
}