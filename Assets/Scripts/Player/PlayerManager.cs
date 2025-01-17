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

        };

    }

    /// <summary>
    /// ©•ª‚Ì‹î‚ğ‘I‘ğ‚·‚é‚Æ‚«
    /// </summary>
    /// <param name="masu"></param>
    private void OnClickMasuAtSelectOwn(Masu masu)
    {
        // ©•ª‚Ì‹î‚ª‰Ÿ‚³‚ê‚½
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
    /// UŒ‚‰Â”\‚È‹î‚ğ‘I‘ğ‚·‚é‚Æ‚«
    /// </summary>
    /// <param name="masu"></param>
    private void OnClickMasuAtAttack(Masu masu)
    {
        print("AAA");
        // ‘Šè‚Ì‹î‚ª‰Ÿ‚³‚ê‚½
        if (_movableWorldPositions.Contains(masu.OwnPosition))
        {
            print("BBB");
            _gameManager.Opponent(_playerNumber).GetComponent<KomaController>().TakeAttack(masu.OwnPosition);
            _clickSystem.OnClickMasu -= OnClickMasuAtAttack;
        }
    }
}