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

public class KomaRebelioonView : MonoBehaviour, IInject<GameManager>, IInject<IReadOnlyList<POWGroupAsset>>
{
    private IReadOnlyList<POWGroupAsset> POWGroupAssets;
    [SerializeField]
    private Image komaFrame;
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
                position.x -= (komaFrame.GetComponent<RectTransform>().lossyScale.x * 2f) * k;
                position.y += (komaFrame.GetComponent<RectTransform>().lossyScale.y * 2f) * i;
                Image image = Instantiate(komaFrame, position, Quaternion.identity, this.transform);

                image.sprite = POWGroupAssets[i].KomaAssets[k].Icon;
            }
        }
    }

    void IInject<IReadOnlyList<POWGroupAsset>>.Inject(IReadOnlyList<POWGroupAsset> t)
    {
        this.POWGroupAssets = t;
    }
}