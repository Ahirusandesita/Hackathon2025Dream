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
    private const int NONE = 0;
    private const int EXIST = 1;

    private Transform _transform = default;
    private int[,] _ban = new int[9,9];

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
    /// <para>GetNonePosition</para>
    /// <para>�����Ȃ��}�X��Ԃ�</para>
    /// </summary>
    /// <returns>�����Ȃ��}�X�i��΍��W�j</returns>
    public Vector2Int[] GetNonePosition()
    {
        // �ԋp�p
        List<Vector2Int> poss = new List<Vector2Int>();
        // ���v���W
        int sumPos = -1;
        // 
        foreach(int banKoma in _ban)
        {
            sumPos++;
            if(banKoma == NONE)
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
    public Vector2Int[] GetMovablePosition(Vector2Int pos, Vector2Int[] movables)
    {
        // �ԋp�p
        List<Vector2Int> poss = new List<Vector2Int>();
        // �ړ��\���W
        foreach(Vector2Int movable in movables)
        {
            Vector2Int checkPos = pos + movable;

            if(_ban[checkPos.y,checkPos.x] == NONE)
            {
                poss.Add(checkPos);
            }
        }
        return poss.ToArray();
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