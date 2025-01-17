// --------------------------------------------------------- 
// BanUI.cs 
// 
// CreateDay: 2025/1/16
// Creator  : Shizuku
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BanUI : MonoBehaviour
{
    #region variable 
    private static BanUI _instatnce = default;

    private Transform _transform = default;
    private Ban _ban = default;
    private BanBlinkObj[] _blinkObjs = default;
    private List<BanBlinkObj> _nowBlink = new List<BanBlinkObj>();

    private float _rightDiff = 0f;
    private float _upDiff = 0.1f;
    private float _forwardDiff = 0f;

    [SerializeField]
    private Color _normalColor = Color.white;
    [SerializeField]
    private Color _movableColor = Color.green;
    [SerializeField]
    private Color _attackableColor = Color.red;
    #endregion

    #region property

    #endregion

    #region singleton
    public static BanUI Get()
    {
        _instatnce = _instatnce != default ? FindAnyObjectByType<BanUI>() : _instatnce;
        return _instatnce;
    }
    #endregion

    #region Unity method
    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        _transform = transform;
        FindAnyObjectByType<ClickSystem>().OnClickMasu += Blink;
        _ban = GetComponent<Ban>();
        Debug.Log(Mathf.FloorToInt(_ban.BanWidth));
        _rightDiff = -(_transform.position.x / _ban.BanHalfWidth);
        _upDiff = _transform.position.y + _upDiff;
        _forwardDiff = (_transform.position.z / _ban.BanHalfHeight);
        _blinkObjs = GetComponentsInChildren<BanBlinkObj>();
    }
    #endregion

    #region method
    /// <summary>
    /// <para>Blink</para>
    /// <para>点滅させる</para>
    /// </summary>
    /// <param name="blinkPositions"></param>
    public void Blink(Vector2Int[] blinkPositions, BlinkColor color = BlinkColor.Normal)
    {
        BlinkOff();

        Vector2Int pos = default;
        foreach(BanBlinkObj blinkObj in _blinkObjs)
        {
            pos.x = _ban.BanHalfWidth + (int)(blinkObj.transform.position.x / _rightDiff);
            pos.y = Mathf.Abs(-_ban.BanHalfHeight + (int)(blinkObj.transform.position.z / _forwardDiff));
            foreach (Vector2Int blinkPos in blinkPositions)
            {
                if(pos == blinkPos)
                {
                    blinkObj.SetColor(GetColorForEnum(color));
                    blinkObj.enabled = true;
                    _nowBlink.Add(blinkObj);
                    break;
                }
            }
        }
    }
    /// <summary>
    /// <para>Blink</para>
    /// <para>点滅させる</para>
    /// </summary>
    /// <param name="blinkPositions"></param>
    public void Blink(Vector2Int blinkPosition, BlinkColor color = BlinkColor.Normal)
    {
        BlinkOff();

        Vector2Int pos = default;
        foreach (BanBlinkObj blinkObj in _blinkObjs)
        {
            pos.x = _ban.BanHalfWidth + (int)(blinkObj.transform.position.x / _rightDiff);
            pos.y = Mathf.Abs(-_ban.BanHalfHeight + (int)(blinkObj.transform.position.z / _forwardDiff));
            Debug.Log(pos + " " + blinkPosition);
            if (pos == blinkPosition)
            {
                blinkObj.SetColor(GetColorForEnum(color));
                blinkObj.enabled = true;
                _nowBlink.Add(blinkObj);
                break;
            }
        }
    }
    #endregion

    #region Get method
    /// <summary>
    /// <para>GetWorldPosition</para>
    /// <para>マス座標をワールド座標に変換します</para>
    /// </summary>
    /// <param name="masuPosition"></param>
    /// <returns></returns>
    public Vector3 GetWorldPosition(Vector2Int masuPosition)
    {
        Vector3 result = Vector3.zero;
        result.x = _rightDiff * -(_ban.BanHalfWidth - masuPosition.x);
        result.y = _upDiff;
        result.z = _forwardDiff * -(masuPosition.y - _ban.BanHalfHeight);
        return result;
    }
    /// <summary>
    /// <para>GetMasuPosition</para>
    /// <para>ワールド座標をマス座標に変換します</para>
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector2Int GetMasuPosition(Vector3 position)
    {
        Vector2Int result = default;
        result.x = _ban.BanHalfWidth + (int)(position.x / _rightDiff);
        result.y = Mathf.Abs(-_ban.BanHalfHeight + (int)(position.z / _forwardDiff));
        return result;
    }

    #endregion

    #region private method
    private void Blink(Masu masu)
    {
        Blink(masu.OwnPosition, BlinkColor.Attack);
    }

    private void BlinkOff()
    {
        // 今点滅しているものがなければ何もしない
        if (_nowBlink.Count == 0)
        {
            return;
        }

        // 今点滅しているものを消灯する
        while (_nowBlink.Count != 0)
        {
            _nowBlink[0].enabled = false;
            _nowBlink.RemoveAt(0);
        }
    }

    private Color GetColorForEnum(BlinkColor color)
    {
        switch(color)
        {
            case BlinkColor.Normal:
                return _normalColor;
            case BlinkColor.Move:
                return _movableColor;
            case BlinkColor.Attack:
                return _attackableColor;
        }
        return _normalColor;
    }
    #endregion
}