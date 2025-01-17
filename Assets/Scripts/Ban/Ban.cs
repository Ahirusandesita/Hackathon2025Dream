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

    [SerializeField]
    private int test = 0;
    [SerializeField]
    private BanUI _banUI = default;
    #endregion

    #region property
    public int BanWidth { get { return _ban.GetLength(0); } }
    public int BanHeight { get { return _ban.GetLength(1); } }
    public int BanHalfWidth { get { return Mathf.FloorToInt(BanWidth / 2); } }
    public int BanHalfHeight { get { return Mathf.FloorToInt(BanHeight / 2); } }
    #endregion

    #region unity method
    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Vector2Int a = GetVectorForInt(test);
            Debug.Log(a);
            _banUI.Blink(a);
        }
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

        Vector2Int[] result = poss.ToArray();
        return result;
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
        Vector2Int[] result = poss.ToArray();
        return result;
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
        PlayerNumber myTeam = _ban[pos.y, pos.x].MyPlayerNumber;

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
            if (_ban[checkPos.y, checkPos.x] is null || myTeam == _ban[checkPos.y, checkPos.x].MyPlayerNumber)
            {
                continue;
            }
            poss.Add(checkPos);
        }
        Vector2Int[] result = poss.ToArray();
        return result;
    }
    #endregion

    #region private method
    /// <summary>
    /// <para>GetVectorForInt</para>
    /// <para>合計座標から座標を算出します</para>
    /// </summary>
    /// <param name="sumPos"></param>
    /// <returns></returns>
    private Vector2Int GetVectorForInt(int sumPos)
    {
        Vector2Int result = default;
        result.x = sumPos % _ban.GetLength(0);
        result.y = sumPos / _ban.GetLength(1);
        return result;
    }

    /// <summary>
    /// <para>CheckPositionInBan</para>
    /// <para>座標が盤上にあるか検査します</para>
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
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