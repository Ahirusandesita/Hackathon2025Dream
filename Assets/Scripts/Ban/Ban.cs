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
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        _transform = transform;
    }
    #endregion

    #region Set method
    /// <summary>
    /// <para>SetKoma</para>
    /// <para>駒を設定します</para>
    /// </summary>
    /// <param name="koma"></param>
    /// <param name="pos"></param>
    public void SetKoma(Koma koma, Vector2Int pos)
    {
        _ban[pos.y, pos.x] = koma;
    }

    /// <summary>
    /// <para>UpdateKomaPos</para>
    /// <para>駒の座標を更新します</para>
    /// </summary>
    /// <param name="oldPos">移動前座標</param>
    /// <param name="newPos">移動後座標</param>
    public void UpdateKomaPos(Vector2Int oldPos, Vector2Int newPos)
    {
        Koma temp = _ban[oldPos.y, oldPos.x];
        _ban[newPos.y, newPos.x] = temp;
        _ban[oldPos.y, oldPos.x] = null;
    }
    #endregion

    #region Get method
    /// <summary>
    /// <para>GetNonePosition</para>
    /// <para>何もないマスを返す</para>
    /// </summary>
    /// <returns>何もないマス（絶対座標）</returns>
    public Vector2Int[] GetNonePosition()
    {
        // 返却用
        List<Vector2Int> poss = new List<Vector2Int>();
        // 合計座標
        int sumPos = -1;
        // 盤上の駒を取得
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
    /// <para>移動可能なマスを返す</para>
    /// </summary>
    /// <param name="pos">現在座標</param>
    /// <param name="movablePositions">移動可能範囲</param>
    /// <returns>移動可能なマス（絶対座標）</returns>
    public Vector2Int[] GetMovablePosition(Vector2Int pos, Vector2Int[] movablePositions)
    {
        // 返却用
        List<Vector2Int> poss = new List<Vector2Int>();
        // 移動可能座標
        foreach(Vector2Int movable in movablePositions)
        {
            Vector2Int checkPos = pos + movable;

            // 盤外
            if (!CheckPositionInBan(checkPos))
            {
                continue;
            }
            // 何もない
            if(_ban[checkPos.y,checkPos.x] is null)
            {
                poss.Add(checkPos);
            }
        }
        return poss.ToArray();
    }

    /// <summary>
    /// <para>GetAttackablePosition</para>
    /// <para>攻撃可能なマスを返す</para>
    /// </summary>
    /// <param name="pos">現在座標</param>
    /// <param name="movablePostions">移動可能範囲</param>
    /// <returns>移動可能なマス（絶対座標）</returns>
    public Vector2Int[] GetAttackablePosition(Vector2Int pos, Vector2Int[] movablePostions)
    {
        // 返却用
        List<Vector2Int> poss = new List<Vector2Int>();
        int teamNo = 0;//_ban[pos.y, pos.x];

        // 移動可能座標
        foreach (Vector2Int movable in movablePostions)
        {
            Vector2Int checkPos = pos + movable;

            // 盤外
            if (!CheckPositionInBan(checkPos))
            {
                continue;
            }
            // 何もない
            if (_ban[checkPos.y, checkPos.x] is null || _ban[checkPos.y, checkPos.x])
            {
                continue;
            }
            poss.Add(checkPos);
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

    private bool CheckPositionInBan(Vector2Int position)
    {
        if(position.x < 0)
        {
            return false;
        }
        if(_ban.Length <= position.x)
        {
            return false;
        }
        if(position.y < 0)
        {
            return false;
        }
        if(((int)_ban.LongLength / _ban.Length) <= position.y)
        {
            return false;
        }
        return true;
    }
    #endregion
}