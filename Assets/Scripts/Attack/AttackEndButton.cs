// --------------------------------------------------------- 
// AttackButton.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;

public class AttackEndButton : MonoBehaviour, IInject<GameManager>
{
    private GameManager gameManager;
    private PlayerNumber playerNumber;

    public void AttackEnd()
    {
        gameManager.GetComponent<IPhaseChanger>().AttackEnd(playerNumber);
        this.gameObject.SetActive(false);
    }

    void IInject<GameManager>.Inject(GameManager gameManager)
    {
        this.gameManager = gameManager;

        this.gameObject.SetActive(false);
    }
}