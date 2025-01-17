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
    private Transform _transform = default;
    private Ban _ban = default;
    [SerializeField]
    private BanBlinkObj[] _blinkObjs = default;
    private List<BanBlinkObj> _nowBlink = new List<BanBlinkObj>();

    [SerializeField]
    private float _rightDiff = 0f;
    [SerializeField]
    private float _forwardDiff = 0f;
    #endregion

    #region property

    #endregion

    #region method
    private void Awake()
    {
        _transform = transform;
        FindAnyObjectByType<ClickSystem>().OnClickMasu += Blink;
        _ban = GetComponent<Ban>();
        Debug.Log(Mathf.FloorToInt(_ban.BanWidth));
        _rightDiff = -(_transform.position.x / _ban.BanHalfWidth);
        _forwardDiff = (_transform.position.z / _ban.BanHalfHeight);
        _blinkObjs = GetComponentsInChildren<BanBlinkObj>();
    }

    /// <summary>
    /// <para>Blink</para>
    /// <para>点滅させる</para>
    /// </summary>
    /// <param name="blinkPositions"></param>
    public void Blink(Vector2Int[] blinkPositions)
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
    public void Blink(Vector2Int blinkPosition)
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
                blinkObj.enabled = true;
                _nowBlink.Add(blinkObj);
                break;
            }
        }
    }

    private void BlinkOff()
    {
        // 今点滅しているものがなければ何もしない
        if(_nowBlink.Count == 0)
        {
            return;
        }

        // 今点滅しているものを消灯する
        while(_nowBlink.Count != 0)
        {
            _nowBlink[0].enabled = false;
            _nowBlink.RemoveAt(0);
        }
    }
    #endregion

    #region private method
    private void Blink(Masu masu)
    {

    }
    #endregion
}