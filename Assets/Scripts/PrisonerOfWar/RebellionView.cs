// --------------------------------------------------------- 
// RebellionView.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;

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
        gameManager.Player1.GetComponent<POWManager>().OnWaitRebellion += async (eventData, sender) =>
        {
            animator.Play("player1");

            await UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("player1"));

            foreach(Koma koma in eventData.Rebellions)
            {
                koma.GetComponent<KomaRebellionEffect>().Rotation();
            }
        };
        gameManager.Player2.GetComponent<POWManager>().OnWaitRebellion += async (eventData, sender) =>
        {
            animator.Play("player2");

            await UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("player2"));

            foreach (Koma koma in eventData.Rebellions)
            {
                koma.GetComponent<KomaRebellionEffect>().Rotation();
            }
        };
    }
}