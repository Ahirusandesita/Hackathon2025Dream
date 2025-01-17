// --------------------------------------------------------- 
// KomaRebelioonView.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class KomaRebelioonView : MonoBehaviour, IInject<GameManager>, IInject<IReadOnlyList<POWGroupAsset>>
{
    private IReadOnlyList<POWGroupAsset> POWGroupAssets;
    [SerializeField]
    private Image komaFrame;
    [SerializeField]
    private TextMeshProUGUI rebellionValueText;

    [SerializeField]
    private GameObject firstPosition;
    private GameManager gameManager;
    void IInject<GameManager>.Inject(GameManager gameManager)
    {
        this.gameManager = gameManager;

        gameManager.OnGameStart += () => Display();
    }

    private void Display()
    {
        Debug.Log(komaFrame.GetComponent<RectTransform>().lossyScale);
        for (int i = 0; i < POWGroupAssets.Count; i++)
        {
            for (int k = 0; k < POWGroupAssets[i].KomaAssets.Count; k++)
            {
                Vector2 position = firstPosition.GetComponent<RectTransform>().position;
                position.x += (komaFrame.GetComponent<RectTransform>().lossyScale.x * 120f) * k;
                position.y -= (komaFrame.GetComponent<RectTransform>().lossyScale.y * 120f) * i;
                Image image = Instantiate(komaFrame, position, Quaternion.identity, this.transform);

                image.sprite = POWGroupAssets[i].KomaAssets[k].Icon;
            }

            Vector2 textPosition = firstPosition.GetComponent<RectTransform>().position;
            textPosition.x += (komaFrame.GetComponent<RectTransform>().lossyScale.x * 120f) * POWGroupAssets[i].KomaAssets.Count;
            textPosition.y -= (komaFrame.GetComponent<RectTransform>().lossyScale.y * 120f) * i;
            TextMeshProUGUI textMeshProUGUI = Instantiate(rebellionValueText, textPosition, Quaternion.identity, this.transform);
            textMeshProUGUI.text = POWGroupAssets[i].NumberOfPOWNeededForRebellion.ToString();
        }
    }

    void IInject<IReadOnlyList<POWGroupAsset>>.Inject(IReadOnlyList<POWGroupAsset> t)
    {
        this.POWGroupAssets = t;
    }
}