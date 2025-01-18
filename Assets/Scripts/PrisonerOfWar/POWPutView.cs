// --------------------------------------------------------- 
// POWPutView.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class POWPutView : MonoBehaviour, IInject<GameManager>
{
    [SerializeField]
    private Image POWImage;
    [SerializeField]
    private TextMeshProUGUI POWText;

    private GameManager gameManager;
    void IInject<GameManager>.Inject(GameManager gameManager)
    {
        this.gameManager = gameManager;

        Initialize();
    }

    private void Initialize()
    {
        POWImage.enabled = false;
        POWText.enabled = false;

        gameManager.Player1.GetComponent<POWManager>().OnPOWPut += (koma) =>
        {
            POWImage.enabled = true;
            POWText.enabled = true;

            POWImage.sprite = koma.KomaAsset.Icon;
            POWText.text = "を配置してください";
        };

        gameManager.Player2.GetComponent<POWManager>().OnPOWPut += (koma) =>
        {
            POWImage.enabled = true;
            POWText.enabled = true;

            POWImage.sprite = koma.KomaAsset.Icon;
            POWText.text = "を配置してください";
        };

        gameManager.Player1.GetComponent<POWManager>().OnPOWPutEnd += () =>
        {
            POWImage.enabled = false;
            POWText.enabled = false;
        };
        gameManager.Player2.GetComponent<POWManager>().OnPOWPutEnd += () =>
        {
            POWImage.enabled = false;
            POWText.enabled = false;
        };
    }
}