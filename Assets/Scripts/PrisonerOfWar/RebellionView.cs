// --------------------------------------------------------- 
// RebellionView.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
public class RebellionView : MonoBehaviour, IInject<GameManager>
{
    [SerializeField]
    private Animator animator;

    private GameManager gameManager;
    void IInject<GameManager>.Inject(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Start()
    {
        gameManager.Player1.GetComponent<POWManager>().OnRebellion += (eventData, sender) =>
        {
            //animator.Play();
        };
        gameManager.Player2.GetComponent<POWManager>().OnRebellion += (eventData, sender) =>
        {

        };
    }
}