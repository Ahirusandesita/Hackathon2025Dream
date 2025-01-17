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
    private float _upDiff = 0.1f;
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
        _upDiff = _transform.position.y + _upDiff;
        _forwardDiff = (_transform.position.z / _ban.BanHalfHeight);
        _blinkObjs = GetComponentsInChildren<BanBlinkObj>();
    }

    /// <summary>
    /// <para>Blink</para>
    /// <para>�_�ł�����</para>
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
    /// <para>�_�ł�����</para>
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

    /// <summary>
    /// <para>GetWorldPosition</para>
    /// <para>�}�X���W�����[���h���W�ɕϊ����܂�</para>
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
    /// <para>���[���h���W���}�X���W�ɕϊ����܂�</para>
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

    }

    private void BlinkOff()
    {
        // ���_�ł��Ă�����̂��Ȃ���Ή������Ȃ�
        if (_nowBlink.Count == 0)
        {
            return;
        }

        // ���_�ł��Ă�����̂���������
        while (_nowBlink.Count != 0)
        {
            _nowBlink[0].enabled = false;
            _nowBlink.RemoveAt(0);
        }

    }
    #endregion
}