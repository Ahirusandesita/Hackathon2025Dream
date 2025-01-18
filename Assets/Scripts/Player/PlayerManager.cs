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

    public PlayerNumber PlayerNomber => _playerNumber;

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

            List<Vector2Int> ownKomaPositions = new List<Vector2Int>();
            foreach (var koma in _komaController.OwnKomas)
            {
                ownKomaPositions.Add(koma.CurrentPosition);
            }

            _clickSystem.OnClickMasu += OnClickMasuAtSelectOwn;
            BanUI.Get().Blink(ownKomaPositions.ToArray());
        };

        _phaseManager = transform.root.GetComponent<PhaseManager>();
        _phaseManager.OnAttackStart += playerNumber =>
        {
            if (playerNumber != _playerNumber)
            {
                return;
            }
        };

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
            _clickSystem.OnClickMasu += OnClickMasuAtMove;
        };

        _phaseManager.OnMoveEnd += playerNumber =>
        {
            if (playerNumber != _playerNumber)
            {
                return;
            }

            _clickSystem.OnClickMasu -= OnClickMasuAtMove;
            _clickSystem.OnClickMasu -= OnClickMasuAtSelectOwn;
        };

        _phaseManager.OnKingMoveStart += playerNumber =>
        {
            if (playerNumber != _playerNumber)
            {
                return;
            }

            // �L���O�擾
            King king = _komaController.GetKing();
            _selectedKomaPosition = king.CurrentPosition;
            _selectedKomaMovableDirections = king.KomaAsset.MovableDirection;
            _clickSystem.OnClickMasu += OnClickMasuAtKingSelect;
            BanUI.Get().Blink(king.CurrentPosition);
        };
    }

    /// <summary>
    /// �����̋��I������Ƃ�
    /// </summary>
    /// <param name="masu"></param>
    private void OnClickMasuAtSelectOwn(Masu masu)
    {
        // �����̋�����ꂽ
        if (_komaController.IsExistOwnKomaAtPosition(masu.OwnPosition, out Vector2Int[] movablePositions))
        {
            _clickSystem.OnClickMasu -= OnClickMasuAtMove;
            _clickSystem.OnClickMasu -= OnClickMasuAtAttack;
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
    /// �U���\�ȃ}�X��I������Ƃ�
    /// </summary>
    /// <param name="masu"></param>
    private void OnClickMasuAtAttack(Masu masu)
    {
        // ����̋�����ꂽ
        if (_attackableWorldPositions.Contains(masu.OwnPosition))
        {
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
    /// �ړ��\�ȃ}�X��I������Ƃ�
    /// </summary>
    /// <param name="masu"></param>
    private void OnClickMasuAtMove(Masu masu)
    {
        // �ړ��\�ȃ}�X�������ꂽ
        if (_movableWorldPositions.Contains(masu.OwnPosition))
        {
            _komaController.MoveKoma(_selectedKomaPosition, masu.OwnPosition).Forget();
            (_phaseManager as IPhaseChanger).MoveEnd(_playerNumber);
        }
    }

    private void OnClickMasuAtKingSelect(Masu masu)
    {
        // �����̋�����ꂽ
        if (_selectedKomaPosition == masu.OwnPosition)
        {
            _attackableWorldPositions = Ban.Get().GetAttackablePosition(masu.OwnPosition, _selectedKomaMovableDirections);
            _clickSystem.OnClickMasu += OnClickMasuAtKingMove;
        }
    }

    private void OnClickMasuAtKingMove(Masu masu)
    {
        // �ړ��\�ȃ}�X�������ꂽ
        if (_movableWorldPositions.Contains(masu.OwnPosition))
        {
            _komaController.MoveKoma(_selectedKomaPosition, masu.OwnPosition).Forget();
            (_phaseManager as IPhaseChanger).MoveEnd(_playerNumber);
        }
    }
}