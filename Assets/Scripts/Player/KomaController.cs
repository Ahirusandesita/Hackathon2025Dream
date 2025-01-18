// --------------------------------------------------------- 
// KomaController.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
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
        // 対局開始イベントを購読し、初期駒を配置
        _gameManager.OnWaitGameStart += SetInitializeKomas;

        //反乱したとき　演出とかするときは演出開始処理をforeachの中に書く
        _gameManager.Opponent(_myPlayerNumber).GetComponent<POWManager>().OnRebellion += (eventData, sender) =>
        {
            foreach (Koma koma in eventData.Rebellions)
            {
                _ownKomas.Add(koma);
            }
        };

        //反乱されたとき　演出とかするときはeventData.Rebillionsを使って演出処理をする
        _gameManager.Me(_myPlayerNumber).GetComponent<POWManager>().OnRebellion += (eventData, sender) =>
        {
            _ownKomas.RemoveAll(item => eventData.Rebellions.Contains(item));
        };

        _phaseManager = FindObjectOfType<PhaseManager>();
    }

    public void InjectPlayer(PlayerNumber playerNumber)
    {
        _myPlayerNumber = playerNumber;
        Initialize();
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

        // もし取られた駒が捕虜だったら、捕虜にしない
        if (_ownKomas[i].MyAbsolutePlayerNumber != _myPlayerNumber)
        {
            _gameManager.Me(_myPlayerNumber).GetComponent<POWManager>().CancelPOW(_ownKomas[i]);
        }
        else
        {
            _gameManager.Opponent(_myPlayerNumber).GetComponent<POWManager>().TurnedIntoPOW(_ownKomas[i]);
        }

        // 相手に駒を渡す
        _gameManager.Opponent(_myPlayerNumber).GetComponent<KomaController>().OwnKomas.Add(_ownKomas[i]);
        _gameManager.Opponent(_myPlayerNumber).GetComponent<KomaWaitingArea>().Arrangement(_ownKomas[i]);
        _ownKomas.RemoveAt(i);
        _ban.RemoveKoma(takeAttackPosition);
    }

    public void MoveKoma(Vector2Int oldPosition, Vector2Int newPosition)
    {
        _ban.UpdateKomaPos(oldPosition, newPosition);
        foreach (var koma in _ownKomas)
        {
            if (oldPosition == koma.CurrentPosition)
            {
                koma.CurrentPosition = newPosition;

                Vector2Int moveDirection = _myPlayerNumber switch
                {
                    PlayerNumber.Player1 => newPosition - oldPosition,
                    PlayerNumber.Player2 => oldPosition - newPosition,
                    _ => throw new System.InvalidProgramException()
                };

                KomaAnimation komaAnimation = koma.GetComponent<KomaAnimation>();

                // ビューの更新
                if (moveDirection == new Vector2Int(1, -1))
                {
                    komaAnimation.Koma_MoveFrontRight();
                }
                else if (moveDirection == new Vector2Int(0, -1))
                {
                    komaAnimation.Koma_MoveFront();
                }
                else if (moveDirection == new Vector2Int(-1, -1))
                {
                    komaAnimation.Koma_MoveFrontLeft();
                }
                else if (moveDirection == new Vector2Int(1, 0))
                {
                    komaAnimation.Koma_MoveRight();
                }
                else if (moveDirection == new Vector2Int(-1, 0))
                {
                    komaAnimation.Koma_MoveLeft();
                }
                else if (moveDirection == new Vector2Int(1, 1))
                {
                    komaAnimation.Koma_BackRight();
                }
                else if (moveDirection == new Vector2Int(-1, 1))
                {
                    komaAnimation.Koma_BackLeft();
                }
                else if (moveDirection == new Vector2Int(0, 1))
                {
                    komaAnimation.Koma_MoveFrontRight();
                }
            }
        }
    }

    /// <summary>
    /// 駒を盤上に配置する
    /// </summary>
    /// <param name="koma"></param>
    /// <param name="newPosition"></param>
    public void PutKoma(Koma koma, Vector2Int newPosition)
    {
        _ownKomas.Add(koma);
        _ban.SetKoma(koma, newPosition);
        koma.CurrentPosition = newPosition;
        koma.transform.position = _banUI.GetWorldPosition(newPosition);
    }

    public King GetKing()
    {
        foreach (var koma in _ownKomas)
        {
            if (koma is King king)
            {
                return king;
            }
        }

        throw new System.InvalidOperationException();
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
            koma.GetComponent<KomaAnimation>().Koma_FallDown().Forget();
            await UniTask.Delay(System.TimeSpan.FromSeconds(0.15));
        }
        transform.root.GetComponent<TurnManager>().TurnStart(PlayerNumber.Player1);
    }
}