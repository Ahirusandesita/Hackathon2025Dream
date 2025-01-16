// --------------------------------------------------------- 
// BanBlinkObj.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BanBlinkObj : MonoBehaviour
{
    #region variable 
    private const float BLINK_TIME = 0.5f;

    private Renderer _render = default;
    private float _timer = BLINK_TIME;
    private int _reverseInt = 1;
    #endregion

    #region property

    #endregion

    #region method
    /// <summary>
    /// ����������
    /// </summary>
    private void Awake()
    {
        _render = GetComponent<Renderer>();
        enabled = false;
    }

    /// <summary>
    /// �L��������
    /// </summary>
    private void OnEnable()
    {
        _render.enabled = true;
        _render.material.color = Color.white;

        _render.material.DOFade(0, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// ����������
    /// </summary>
    private void OnDisable()
    {
        _render.material.DOKill();
        _render.enabled = false;
    }
    #endregion
}