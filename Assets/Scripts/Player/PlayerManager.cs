// --------------------------------------------------------- 
// PlayerManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using Cysharp.Threading.Tasks.Triggers;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerNumber _playerNumber = default;
    [SerializeField]
    private KomaController _komaController = default;
    [SerializeField]
    private AttackEndButton _attackEndButton = default;
    private ClickSystem _clickSystem = default;
    private GameManager _gameManager = default;
    private PhaseManager _phaseManager = default;
    private Vector2Int[] _attackableWorldPositions = default;
    private Vector2Int[] _movableWorldPositions = default;
    private Vector2Int _selectedKomaPosition = default;
    private Vector2Int[] _selectedKomaMovableDirections = default;
    private bool _isFirstMove = default;
    private bool _isMoved = default;
    private bool _isAttack = default;

    public PlayerNumber PlayerNumber => _playerNumber;

    public static int GetMoveDirectionCoefficient(PlayerNumber playerNumber)
    {
        switch (playerNumber)
        {
            case PlayerNumber.Player1:
                return -1;
            case PlayerNumber.Player2:
                return 1;
            default:
                throw new System.ArgumentOutOfRangeException();
        }
    }

    private void Awake()
    {
        _gameManager = transform.root.GetComponent<GameManager>();
        _clickSystem = FindObjectOfType<ClickSystem>();
        TurnManager turnManager = transform.root.GetComponent<TurnManager>();
        turnManager.OnTurnStart += playerNumber =>
        {
            if (playerNumber != _playerNumber)
            {
                return;
            }

            _isFirstMove = true;
            _isMoved = false;
            _isAttack = false;
            List<Vector2Int> ownKomaPositions = new List<Vector2Int>();
            foreach (var koma in _komaController.OwnKomas)
            {
                ownKomaPositions.Add(koma.CurrentPosition);
            }

            _clickSystem.OnClickMasu = null;
            _clickSystem.OnClickMasu += OnClickMasuAtSelectOwn;
            BanUI.Get().Blink(ownKomaPositions.ToArray());
        };

        _phaseManager = transform.root.GetComponent<PhaseManager>();

        _phaseManager.OnAttackEnd += playerNumber =>
        {
            if (playerNumber != _playerNumber)
            {
                return;
            }

            _clickSystem.OnClickMasu -= OnClickMasuAtAttack;
        };

        _phaseManager.OnMoveStart += playerNumber =>
        {
            if (playerNumber != _playerNumber)
            {
                return;
            }

            _movableWorldPositions = Ban.Get().GetMovablePosition(_selectedKomaPosition, _selectedKomaMovableDirections);
            if (_isFirstMove)
            {
                _clickSystem.OnClickMasu += OnClickMasuAtMove;
            }
            _isFirstMove = false;
        };

        _phaseManager.OnMoveEnd += playerNumber =>
        {
            if (playerNumber != _playerNumber)
            {
                return;
            }

            _clickSystem.OnClickMasu -= OnClickMasuAtMove;
            _clickSystem.OnClickMasu -= OnClickMasuAtSelectOwn;
            _clickSystem.OnClickMasu -= OnClickMasuAtAttack;
        };

        _phaseManager.OnKingMoveStart += playerNumber =>
        {
            if (playerNumber != _playerNumber)
            {
                return;
            }
            _clickSystem.OnClickMasu -= OnClickMasuAtSelectOwn;
            // キング取得
            King king = _komaController.GetKing();
            _selectedKomaPosition = king.CurrentPosition;
            _selectedKomaMovableDirections = king.KomaAsset.MovableDirection;
            _movableWorldPositions = Ban.Get().GetMovablePosition(_selectedKomaPosition, _selectedKomaMovableDirections);
            // 移動可能場所がなかったら負け
            if (_movableWorldPositions.Length == 0)
            {
                _gameManager.GameEnd(_gameManager.Opponent(_playerNumber).GetComponent<PlayerManager>().PlayerNumber);
                return;
            }
            _clickSystem.OnClickMasu += OnClickMasuAtKingMove;
        };
    }

    /// <summary>
    /// 自分の駒を選択するとき
    /// </summary>
    /// <param name="masu"></param>
    private void OnClickMasuAtSelectOwn(Masu masu)
    {
        // 自分の駒が押された
        if (_komaController.IsExistOwnKomaAtPosition(masu.OwnPosition, out Vector2Int[] movablePositions) && !_isMoved && !_isAttack)
        {
            //_clickSystem.OnClickMasu -= OnClickMasuAtMove;
            //_clickSystem.OnClickMasu -= OnClickMasuAtAttack;
            _selectedKomaPosition = masu.OwnPosition;
            _selectedKomaMovableDirections = movablePositions;
            _attackableWorldPositions = Ban.Get().GetAttackablePosition(masu.OwnPosition, movablePositions);
            if (_attackableWorldPositions.Length == 0)
            {
                (_phaseManager as IPhaseChanger).MoveStart(_playerNumber);
                _attackEndButton.gameObject.SetActive(false);
            }
            else
            {
                (_phaseManager as IPhaseChanger).AttackStart(_playerNumber);
                _clickSystem.OnClickMasu += OnClickMasuAtAttack;
                _attackEndButton.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 攻撃可能なマスを選択するとき
    /// </summary>
    /// <param name="masu"></param>
    private void OnClickMasuAtAttack(Masu masu)
    {
        // 相手の駒が押された
        if (_attackableWorldPositions.Contains(masu.OwnPosition))
        {
            _isAttack = true;
            KomaController opponent = _gameManager.Opponent(_playerNumber).GetComponent<KomaController>();
            opponent.TakeAttack(masu.OwnPosition);
            _attackableWorldPositions = Ban.Get().GetAttackablePosition(_selectedKomaPosition, _selectedKomaMovableDirections);
            if (_attackableWorldPositions.Length == 0)
            {
                (_phaseManager as IPhaseChanger).AttackEnd(_playerNumber);
            }
        }
    }

    /// <summary>
    /// 移動可能なマスを選択するとき
    /// </summary>
    /// <param name="masu"></param>
    private void OnClickMasuAtMove(Masu masu)
    {
        // 移動可能なマスが押された
        if (_movableWorldPositions.Contains(masu.OwnPosition))
        {
            _clickSystem.OnClickMasu -= OnClickMasuAtSelectOwn;
            _isMoved = true;
            _komaController.MoveKoma(_selectedKomaPosition, masu.OwnPosition).Forget();
            (_phaseManager as IPhaseChanger).MoveEnd(_playerNumber);
        }
    }

    private void OnClickMasuAtKingMove(Masu masu)
    {
        // 移動可能なマスが押された
        if (_movableWorldPositions.Contains(masu.OwnPosition))
        {
            _komaController.MoveKoma(_selectedKomaPosition, masu.OwnPosition).Forget();
            (_phaseManager as IPhaseChanger).KingMoveEnd(_playerNumber);
            print("AA");
        }
    }
}