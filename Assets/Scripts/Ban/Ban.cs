// --------------------------------------------------------- 
// Ban.cs 
// 
// CreateDay: 2025/1/16
// Creator  : Shizuku
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ban : MonoBehaviour
{
    #region variable 
    private Transform _transform = default;
    private Koma[,] _ban = new Koma[9,9];

    #endregion

    #region property

    #endregion

    #region unity method
    /// <summary>
    /// ����������
    /// </summary>
    private void Awake()
    {
        _transform = transform;
    }
    #endregion

    #region Get method
    /// <summary>
    /// <para>GetInteractablePosition</para>
    /// <para>����\�ȃ}�X��Ԃ�</para>
    /// </summary>
    /// <returns>����\�ȃ}�X�i��΍��W�j</returns>
    public Vector2Int[] GetInteractablePosition()
    {
        List<Vector2Int> poss = new List<Vector2Int>();

        int sumPos = -1;
        foreach(Koma banKoma in _ban)
        {
            sumPos++;
            if(banKoma is null)
            {
                poss.Add(GetVectorForInt(sumPos));
            }
        }

        return poss.ToArray();
    }

    /// <summary>
    /// <para>GetMovablePosition</para>
    /// <para>�ړ��\�ȃ}�X��Ԃ�</para>
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="koma"></param>
    /// <returns>�ړ��\�ȃ}�X�i��΍��W�j</returns>
    public Vector2Int[] GetMovablePosition(Vector2Int pos, Koma koma)
    {
        List<Vector2Int> poss = new List<Vector2Int>();


        return default;
    }

    /// <summary>
    /// <para>GetNowPosition</para>
    /// <para>���݂̋�̃}�X��Ԃ�</para>
    /// </summary>
    /// <param name="koma"></param>
    /// <returns>���ݍ��W</returns>
    public Vector2Int GetNowPosition(Koma koma)
    {
        int sumPos = -1;
        // �Ղ̋��
        foreach(Koma banKoma in _ban)
        {
            sumPos++;
            if(banKoma == koma)
            {
                break;
            }
        }
        Vector2Int result = GetVectorForInt(sumPos);

        return result;
    }
    #endregion

    #region private method
    private Vector2Int GetVectorForInt(int sumPos)
    {
        Vector2Int result = default;
        result.x = sumPos % _ban.Length;
        result.y = sumPos / ((int)_ban.LongLength / _ban.Length);
        return result;
    }
    #endregion
}