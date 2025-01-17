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

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerNumber _playerNumber = default;
    [SerializeField]
    private KomaController _komaController = default;
    private Ban _ban = default;
    private ClickSystem _clickSystem = default;
    private GameManager _gameManager = default;
    private Vector2Int[] _movableWorldPositions = default;

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
        _ban = FindObjectOfType<Ban>();
        _clickSystem = FindObjectOfType<ClickSystem>();

        PhaseManager phaseManager = transform.root.GetComponent<PhaseManager>();
        phaseManager.OnAttackStart += playerNumber =>
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
            FindObjectOfType<BanUI>().Blink(ownKomaPositions.ToArray());
        };

        phaseManager.OnAttackEnd += playerNumber =>
        {
            if (playerNumber != _playerNumber)
            {
                return;
            }
        };

    }

    /// <summary>
    /// 自分の駒を選択するとき
    /// </summary>
    /// <param name="masu"></param>
    private void OnClickMasuAtSelectOwn(Masu masu)
    {
        // 自分の駒が押された
        if (_komaController.IsExistOwnKomaAtPosition(masu.OwnPosition, out Vector2Int[] movablePosition))
        {
            _movableWorldPositions = _ban.GetAttackablePosition(masu.OwnPosition, movablePosition);
            print(movablePosition.Length);
            foreach (var item in _movableWorldPositions)
            {
                print(item);
            }
            _clickSystem.OnClickMasu += OnClickMasuAtAttack;
            _clickSystem.OnClickMasu -= OnClickMasuAtSelectOwn;
        }
    }

    /// <summary>
    /// 攻撃可能な駒を選択するとき
    /// </summary>
    /// <param name="masu"></param>
    private void OnClickMasuAtAttack(Masu masu)
    {
        // 相手の駒が押された
        if (_movableWorldPositions.Contains(masu.OwnPosition))
        {
            _gameManager.Opponent(_playerNumber).GetComponent<KomaController>().TakeAttack(masu.OwnPosition);
            // ここで攻撃可能な駒があるかチェック
            _clickSystem.OnClickMasu -= OnClickMasuAtAttack;
        }
    }
}