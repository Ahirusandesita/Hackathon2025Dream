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
    private List<BanBlinkObj> _blinkObjs = new List<BanBlinkObj>();
    private List<BanBlinkObj> _nowBlink = new List<BanBlinkObj>();

    [SerializeField]
    private GameObject _blinkObj = default;
    #endregion

    #region property

    #endregion

    #region method
    private void Awake()
    {
        _transform = transform;
        Vector3 rightDiff = Vector3.right * -(_transform.position.x / 4);
        Vector3 upDiff = Vector3.up * 0.1f;
        Vector3 forwardDiff = Vector3.forward * -(_transform.position.z / 4);
        Debug.Log(rightDiff + forwardDiff);
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Instantiate(_blinkObj, _transform.position + rightDiff * i + upDiff + forwardDiff * j, Quaternion.identity, _transform);
            }
        }

        _blinkObjs.AddRange(GetComponentsInChildren<BanBlinkObj>());
    }

    /// <summary>
    /// <para>Blink</para>
    /// <para>ì_ñ≈Ç≥ÇπÇÈ</para>
    /// </summary>
    /// <param name="blinkPositions"></param>
    public void Blink(Vector2Int[] blinkPositions)
    {
        BlinkOff();

        Vector2Int pos = default;
        foreach(BanBlinkObj blinkObj in _blinkObjs)
        {
            pos.x = (int)blinkObj.transform.position.x;
            pos.y = (int)blinkObj.transform.position.z;
            foreach(Vector2Int blinkPos in blinkPositions)
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

    private void BlinkOff()
    {
        // ç°ì_ñ≈ÇµÇƒÇ¢ÇÈÇ‡ÇÃÇ™Ç»ÇØÇÍÇŒâΩÇ‡ÇµÇ»Ç¢
        if(_nowBlink.Count == 0)
        {
            return;
        }

        // ç°ì_ñ≈ÇµÇƒÇ¢ÇÈÇ‡ÇÃÇè¡ìîÇ∑ÇÈ
        while(_nowBlink.Count != 0)
        {
            _nowBlink[0].enabled = false;
            _blinkObjs.Add(_nowBlink[0]);
        }
    }
    #endregion
}