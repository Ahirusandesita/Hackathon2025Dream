// --------------------------------------------------------- 
// BanBlinkObj.cs 
// 
// CreateDay: 2025/1/16
// Creator  : Shizuku
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BanBlinkObj : MonoBehaviour
{
    #region variable 
    private Transform _transform = default;
    private Renderer _render = default;
    private Vector3 _scale = default;
    #endregion

    #region property

    #endregion

    #region Unity method
    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        _transform = transform;
        _render = GetComponent<Renderer>();
        _scale = _transform.localScale;
        enabled = false;
    }

    /// <summary>
    /// 有効化処理
    /// </summary>
    private void OnEnable()
    {
        _render.enabled = true;
        _render.material.DOFade(0, 0.6f).SetLoops(-1,LoopType.Yoyo);

    }

    /// <summary>
    /// 無効化処理
    /// </summary>
    private void OnDisable()
    {
        _render.material.DOKill();
        _render.enabled = false;
    }
    #endregion

    #region Set method
    /// <summary>
    /// <para>SetColor</para>
    /// <para>色を設定します</para>
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color)
    {
        _render.material.color = color;
    }
    #endregion
}