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
    

    private const int CAMPRANGE = 3;
    private static Ban _instance = default;

    [SerializeField]
    private int _kingArea = 1;
    [SerializeField]
    private Koma _player1King = default;
    [SerializeField]
    private Koma _player2King = default;
    private Koma[,] _ban = new Koma[9,9];

    [SerializeField,Header("Debug")]
    private int _test = 0;
    [SerializeField]
    private PlayerNumber _tePl = default;
    #endregion

    #region property
    public int BanWidth { get { return _ban.GetLength(0); } }
    public int BanHeight { get { return _ban.GetLength(1); } }
    public int BanHalfWidth { get { return Mathf.FloorToInt(BanWidth / 2); } }
    public int BanHalfHeight { get { return Mathf.FloorToInt(BanHeight / 2); } }
    #endregion

    #region singleton
    public static Ban Get()
    {
        _instance = _instance == default ? FindAnyObjectByType<Ban>() : _instance;
        return _instance;
    }
    #endregion

    #region unity method
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            string a = "";
            for(int y = 0;y < BanHeight; y++)
            {
                for(int x = 0;x < BanWidth; x++)
                {
                    Debug.Log(x + "," + y + " :" + _ban[y,x]);
                }
            }
            
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            CheckKingAreaPosition(new Vector2Int(0, 0), _tePl);
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

        // 置かれた駒がキングではない
        if(!koma.TryGetComponent(out King king))
        {
            return;
        }
        // プレイヤー対応
        if (king.MyPlayerNumber == PlayerNumber.Player1)
        {
            _player1King = king;
        }
        else
        {
            _player2King = king;
        }
    }

    /// <summary>
    /// <para>RemoveKoma</para>
    /// <para>駒を削除します</para>
    /// </summary>
    /// <param name="pos"></param>
    public void RemoveKoma(Vector2Int pos)
    {
        _ban[pos.y, pos.x] = default;
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
        _ban[oldPos.y, oldPos.x] = _ban[newPos.y, newPos.x];
        _ban[newPos.y, newPos.x] = temp;

        //Debug.Log(oldPos + ":" + _ban[oldPos.y, oldPos.x] + " >> " + newPos + ":" + _ban[newPos.y, newPos.x]);
    }
    #endregion

    #region Get method
    /// <summary>
    /// <para>CheckPosition</para>
    /// <para>指定した座標が何もないか検査します</para>
    /// </summary>
    /// <param name="checkPos"></param>
    /// <returns></returns>
    public bool CheckPosition(Vector2Int checkPos)
    {
        // 盤外
        if (!CheckPositionInBan(checkPos))
        {
            return false;
        }
        // 何もない
        if (_ban[checkPos.y, checkPos.x] is null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// <para>CheckPutPosition</para>
    /// <para></para>
    /// </summary>
    /// <param name="checkPos"></param>
    /// <param name="team"></param>
    /// <returns></returns>
    public bool CheckPutPosition(Vector2Int checkPos, PlayerNumber team)
    {
        // 盤外
        if (!CheckPositionInBan(checkPos))
        {
            return false;
        }
        // 王の範囲内
        if (CheckKingAreaPosition(checkPos,team))
        {
            return false;
        }
        // 何もない
        if (_ban[checkPos.y, checkPos.x] is null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// <para>CheckPosition</para>
    /// <para>指定した座標が王の周りにあるか検査します</para>
    /// </summary>
    /// <param name="checkPos"></param>
    /// <returns>範囲内判定</returns>
    public bool CheckKingAreaPosition(Vector2Int checkPos, PlayerNumber team)
    {
        // 盤外
        if (!CheckPositionInBan(checkPos))
        {
            return false;
        }

        // 対象の王
        Vector2Int targetKingPos = default;
        if (team == PlayerNumber.Player1)
        {
            targetKingPos = _player2King.CurrentPosition;
        }
        else
        {
            targetKingPos = _player1King.CurrentPosition;
        }

        // 王の周辺を検索
        List<Vector2Int> areaPos = new List<Vector2Int>();
        for(int y = -_kingArea;y <= _kingArea;y++)
        {
            for(int x = -_kingArea;x <= _kingArea;x++)
            {
                Vector2Int checkKingAreaPos = targetKingPos;
                checkKingAreaPos.y += y;
                checkKingAreaPos.x += x;
                // 盤外
                if (!CheckPositionInBan(checkKingAreaPos))
                {
                    continue;
                }
                areaPos.Add(checkKingAreaPos);
            }
        }

        return areaPos.Contains(checkPos);
    }

    /// <summary>
    /// <para>CheckEnemyCamp</para>
    /// <para>指定した座標が敵陣内かどうか検査します</para>
    /// </summary>
    /// <returns></returns>
    public bool CheckEnemyCamp(Vector2Int checkPos, PlayerNumber player)
    {
        // 敵陣にいるか
        if(player == PlayerNumber.Player1 && CAMPRANGE <= checkPos.y)
        {
            return false;
        }
        else if(player == PlayerNumber.Player2 && checkPos.y <= BanHeight - CAMPRANGE)
        {
            return false;
        }

        // 盤外
        if (!CheckPositionInBan(checkPos))
        {
            return false;
        }

        return true;
    }

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
        BanUI.Get().Blink(result, BlinkColor.Normal);
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
        // 駒取得
        Koma koma = _ban[pos.y, pos.x];
        if(_ban[pos.y,pos.x] is null)
        {
            Debug.LogWarning(pos + " is Null");
            return default;
        }
        //Debug.Log("M" + pos + " " + _ban[pos.y, pos.x]);
        // 相対変換
        PlayerNumber myTeam = koma.MyPlayerNumber;
        int dire = PlayerManager.GetMoveDirectionCoefficient(myTeam);
        Vector2Int[] direMovable = (Vector2Int[])movablePositions.Clone(); 
        for(int i = 0;i<direMovable.Length;i++)
        {
            direMovable[i] *= dire;
            
        }

        // 駒の直線処理が必要である
        if(koma.KomaAsset.CollidableDirection.Length != 0)
        {
            Vector2Int[] collideDire = (Vector2Int[])koma.KomaAsset.CollidableDirection.Clone();
            for (int i = 0; i < collideDire.Length; i++)
            {
                collideDire[i] = collideDire[i] * dire;
            }
            poss.AddRange(GetCollideList(pos, movablePositions, collideDire, false));
        }
        else
        {
            // 移動可能座標
            foreach (Vector2Int movable in direMovable)
            {
                Vector2Int checkPos = pos + movable;

                // 盤外
                if (!CheckPositionInBan(checkPos))
                {
                    continue;
                }
                // 何もない
                if (_ban[checkPos.y, checkPos.x] is null)
                {
                    poss.Add(checkPos);
                }
            }
        }
        Vector2Int[] result = poss.ToArray();
        BanUI.Get().Blink(result, BlinkColor.Move);
        return result;
    }

    /// <summary>
    /// <para>GetAttackablePosition</para>
    /// <para>攻撃可能なマスを返す</para>
    /// </summary>
    /// <param name="pos">現在座標</param>
    /// <param name="movablePositions">移動可能範囲</param>
    /// <returns>移動可能なマス（絶対座標）</returns>
    public Vector2Int[] GetAttackablePosition(Vector2Int pos, Vector2Int[] movablePositions)
    {
        // 返却用
        List<Vector2Int> poss = new List<Vector2Int>();
        // 駒取得
        Koma koma = _ban[pos.y, pos.x];
        if (_ban[pos.y, pos.x] is null)
        {
            Debug.LogWarning(pos + " is Null");
            return default;
        }
        //Debug.Log("A" + pos + " " + _ban[pos.y, pos.x]);
        // 相対変換
        PlayerNumber myTeam = koma.MyPlayerNumber;
        int dire = PlayerManager.GetMoveDirectionCoefficient(myTeam);
        Vector2Int[] direMovable = (Vector2Int[])movablePositions.Clone();
        for (int i = 0; i < direMovable.Length; i++)
        {
            direMovable[i] *= dire;

        }

        //Debug.Log(movablePostions.Length);
        // 駒の直線処理が必要である
        if (koma.KomaAsset.CollidableDirection.Length != 0)
        {
            // 相対変換
            Vector2Int[] collideDire = (Vector2Int[])koma.KomaAsset.CollidableDirection.Clone();
            for (int i = 0; i < collideDire.Length; i++)
            {
                collideDire[i] = collideDire[i] * dire;
            }
            // 取得
            Vector2Int[] collides = GetCollideList(pos, movablePositions, collideDire, true);
            // 検査
            foreach(Vector2Int collide in collides)
            {
                // 盤外
                if (!CheckPositionInBan(collide))
                {
                    continue;
                }
                // 何もない
                if (myTeam == _ban[collide.y, collide.x].MyPlayerNumber)
                {
                    continue;
                }
                poss.Add(collide);
            }
        }
        else
        {
            // 検査
            foreach (Vector2Int movable in direMovable)
            {
                Vector2Int checkPos = pos + movable;
                //Debug.Log(checkPos);
                // 盤外
                if (!CheckPositionInBan(checkPos))
                {
                    //Debug.Log("none");
                    continue;
                }
                // 何もない
                if (_ban[checkPos.y, checkPos.x] is null || myTeam == _ban[checkPos.y, checkPos.x].MyPlayerNumber)
                {
                    //Debug.Log("null or myteam");
                    continue;
                }
                poss.Add(checkPos);
            }
        }
        Vector2Int[] result = poss.ToArray();
        BanUI.Get().Blink(result, BlinkColor.Attack);
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
        if(BanWidth <= position.x)
        {
            return false;
        }
        if(position.y < 0)
        {
            return false;
        }
        if(BanHeight <= position.y)
        {
            return false;
        }
        return true;
    }

    private Vector2Int[] GetCollideList(Vector2Int pos, Vector2Int[] movablePositions, Vector2Int[] dire, bool isCollide)
    {
        // 初期化
        List<Vector2Int> result = new List<Vector2Int>();
        List<Vector2Int>[] collide = new List<Vector2Int>[dire.Length];
        for(int i = 0;i<dire.Length;i++)
        {
            collide[i] = new List<Vector2Int>();
        }

        // 各座標を検査
        foreach (Vector2Int movable in movablePositions)
        {
            Vector2Int checkPos = pos + movable;
            // 盤外
            if (!CheckPositionInBan(checkPos))
            {
                continue;
            }
            // 方向リスト
            for (int i = 0; i < dire.Length; i++)
            {
                // 移動可能マスが入っていない かつ 自身の座標の1マス先ではない
                if (collide[i].Count == 0 && checkPos != pos + dire[i])
                {
                    continue;
                }
                // 移動可能マスが入っている かつ 移動可能マスの1マス先ではない
                if(collide[i].Count != 0 && checkPos != collide[i][collide[i].Count - 1] + dire[i])
                {
                    continue;
                }
                // 何もない
                if(_ban[checkPos.y,checkPos.x] is null)
                {
                    collide[i].Add(checkPos);
                }
            }
        }

        // 衝突先を取得するか
        if(isCollide)
        {
            // 各方向のリストを合算
            for (int i = 0; i < dire.Length; i++)
            {
                // 進めるマスがない
                if(collide[i].Count == 0)
                {
                    // 1マス先の座標を設定
                    result.Add(pos + dire[i]);
                    continue;
                }
                // 衝突先の座標を設定
                result.Add(collide[i][collide[i].Count - 1] + dire[i]);
            }
        }
        else
        {
            // 各方向のリストを合算
            for (int i = 0; i < dire.Length; i++)
            {
                result.AddRange(collide[i]);
            }
        }

        return result.ToArray();
    }
    #endregion
}