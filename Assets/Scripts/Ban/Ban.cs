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
    /// <para>���ݒ肵�܂�</para>
    /// </summary>
    /// <param name="koma"></param>
    /// <param name="pos"></param>
    public void SetKoma(Koma koma, Vector2Int pos)
    {
        _ban[pos.y, pos.x] = koma;

        // �u���ꂽ��L���O�ł͂Ȃ�
        if(!koma.TryGetComponent(out King king))
        {
            return;
        }
        // �v���C���[�Ή�
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
    /// <para>����폜���܂�</para>
    /// </summary>
    /// <param name="pos"></param>
    public void RemoveKoma(Vector2Int pos)
    {
        _ban[pos.y, pos.x] = default;
    }

    /// <summary>
    /// <para>UpdateKomaPos</para>
    /// <para>��̍��W���X�V���܂�</para>
    /// </summary>
    /// <param name="oldPos">�ړ��O���W</param>
    /// <param name="newPos">�ړ�����W</param>
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
    /// <para>�w�肵�����W�������Ȃ����������܂�</para>
    /// </summary>
    /// <param name="checkPos"></param>
    /// <returns></returns>
    public bool CheckPosition(Vector2Int checkPos)
    {
        // �ՊO
        if (!CheckPositionInBan(checkPos))
        {
            return false;
        }
        // �����Ȃ�
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
        // �ՊO
        if (!CheckPositionInBan(checkPos))
        {
            return false;
        }
        // ���͈͓̔�
        if (CheckKingAreaPosition(checkPos,team))
        {
            return false;
        }
        // �����Ȃ�
        if (_ban[checkPos.y, checkPos.x] is null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// <para>CheckPosition</para>
    /// <para>�w�肵�����W�����̎���ɂ��邩�������܂�</para>
    /// </summary>
    /// <param name="checkPos"></param>
    /// <returns>�͈͓�����</returns>
    public bool CheckKingAreaPosition(Vector2Int checkPos, PlayerNumber team)
    {
        // �ՊO
        if (!CheckPositionInBan(checkPos))
        {
            return false;
        }

        // �Ώۂ̉�
        Vector2Int targetKingPos = default;
        if (team == PlayerNumber.Player1)
        {
            targetKingPos = _player2King.CurrentPosition;
        }
        else
        {
            targetKingPos = _player1King.CurrentPosition;
        }

        // ���̎��ӂ�����
        List<Vector2Int> areaPos = new List<Vector2Int>();
        for(int y = -_kingArea;y <= _kingArea;y++)
        {
            for(int x = -_kingArea;x <= _kingArea;x++)
            {
                Vector2Int checkKingAreaPos = targetKingPos;
                checkKingAreaPos.y += y;
                checkKingAreaPos.x += x;
                // �ՊO
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
    /// <para>�w�肵�����W���G�w�����ǂ����������܂�</para>
    /// </summary>
    /// <returns></returns>
    public bool CheckEnemyCamp(Vector2Int checkPos, PlayerNumber player)
    {
        // �G�w�ɂ��邩
        if(player == PlayerNumber.Player1 && CAMPRANGE <= checkPos.y)
        {
            return false;
        }
        else if(player == PlayerNumber.Player2 && checkPos.y <= BanHeight - CAMPRANGE)
        {
            return false;
        }

        // �ՊO
        if (!CheckPositionInBan(checkPos))
        {
            return false;
        }

        return true;
    }

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
        // �Տ�̋���擾
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
    /// <para>�ړ��\�ȃ}�X��Ԃ�</para>
    /// </summary>
    /// <param name="pos">���ݍ��W</param>
    /// <param name="movablePositions">�ړ��\�͈�</param>
    /// <returns>�ړ��\�ȃ}�X�i��΍��W�j</returns>
    public Vector2Int[] GetMovablePosition(Vector2Int pos, Vector2Int[] movablePositions)
    {
        // �ԋp�p
        List<Vector2Int> poss = new List<Vector2Int>();
        // ��擾
        Koma koma = _ban[pos.y, pos.x];
        if(_ban[pos.y,pos.x] is null)
        {
            Debug.LogWarning(pos + " is Null");
            return default;
        }
        //Debug.Log("M" + pos + " " + _ban[pos.y, pos.x]);
        // ���Εϊ�
        PlayerNumber myTeam = koma.MyPlayerNumber;
        int dire = PlayerManager.GetMoveDirectionCoefficient(myTeam);
        Vector2Int[] direMovable = (Vector2Int[])movablePositions.Clone(); 
        for(int i = 0;i<direMovable.Length;i++)
        {
            direMovable[i] *= dire;
            
        }

        // ��̒����������K�v�ł���
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
            // �ړ��\���W
            foreach (Vector2Int movable in direMovable)
            {
                Vector2Int checkPos = pos + movable;

                // �ՊO
                if (!CheckPositionInBan(checkPos))
                {
                    continue;
                }
                // �����Ȃ�
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
    /// <para>�U���\�ȃ}�X��Ԃ�</para>
    /// </summary>
    /// <param name="pos">���ݍ��W</param>
    /// <param name="movablePositions">�ړ��\�͈�</param>
    /// <returns>�ړ��\�ȃ}�X�i��΍��W�j</returns>
    public Vector2Int[] GetAttackablePosition(Vector2Int pos, Vector2Int[] movablePositions)
    {
        // �ԋp�p
        List<Vector2Int> poss = new List<Vector2Int>();
        // ��擾
        Koma koma = _ban[pos.y, pos.x];
        if (_ban[pos.y, pos.x] is null)
        {
            Debug.LogWarning(pos + " is Null");
            return default;
        }
        //Debug.Log("A" + pos + " " + _ban[pos.y, pos.x]);
        // ���Εϊ�
        PlayerNumber myTeam = koma.MyPlayerNumber;
        int dire = PlayerManager.GetMoveDirectionCoefficient(myTeam);
        Vector2Int[] direMovable = (Vector2Int[])movablePositions.Clone();
        for (int i = 0; i < direMovable.Length; i++)
        {
            direMovable[i] *= dire;

        }

        //Debug.Log(movablePostions.Length);
        // ��̒����������K�v�ł���
        if (koma.KomaAsset.CollidableDirection.Length != 0)
        {
            // ���Εϊ�
            Vector2Int[] collideDire = (Vector2Int[])koma.KomaAsset.CollidableDirection.Clone();
            for (int i = 0; i < collideDire.Length; i++)
            {
                collideDire[i] = collideDire[i] * dire;
            }
            // �擾
            Vector2Int[] collides = GetCollideList(pos, movablePositions, collideDire, true);
            // ����
            foreach(Vector2Int collide in collides)
            {
                // �ՊO
                if (!CheckPositionInBan(collide))
                {
                    continue;
                }
                // �����Ȃ�
                if (myTeam == _ban[collide.y, collide.x].MyPlayerNumber)
                {
                    continue;
                }
                poss.Add(collide);
            }
        }
        else
        {
            // ����
            foreach (Vector2Int movable in direMovable)
            {
                Vector2Int checkPos = pos + movable;
                //Debug.Log(checkPos);
                // �ՊO
                if (!CheckPositionInBan(checkPos))
                {
                    //Debug.Log("none");
                    continue;
                }
                // �����Ȃ�
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
    /// <para>���v���W������W���Z�o���܂�</para>
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
    /// <para>���W���Տ�ɂ��邩�������܂�</para>
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
        // ������
        List<Vector2Int> result = new List<Vector2Int>();
        List<Vector2Int>[] collide = new List<Vector2Int>[dire.Length];
        for(int i = 0;i<dire.Length;i++)
        {
            collide[i] = new List<Vector2Int>();
        }

        // �e���W������
        foreach (Vector2Int movable in movablePositions)
        {
            Vector2Int checkPos = pos + movable;
            // �ՊO
            if (!CheckPositionInBan(checkPos))
            {
                continue;
            }
            // �������X�g
            for (int i = 0; i < dire.Length; i++)
            {
                // �ړ��\�}�X�������Ă��Ȃ� ���� ���g�̍��W��1�}�X��ł͂Ȃ�
                if (collide[i].Count == 0 && checkPos != pos + dire[i])
                {
                    continue;
                }
                // �ړ��\�}�X�������Ă��� ���� �ړ��\�}�X��1�}�X��ł͂Ȃ�
                if(collide[i].Count != 0 && checkPos != collide[i][collide[i].Count - 1] + dire[i])
                {
                    continue;
                }
                // �����Ȃ�
                if(_ban[checkPos.y,checkPos.x] is null)
                {
                    collide[i].Add(checkPos);
                }
            }
        }

        // �Փː���擾���邩
        if(isCollide)
        {
            // �e�����̃��X�g�����Z
            for (int i = 0; i < dire.Length; i++)
            {
                // �i�߂�}�X���Ȃ�
                if(collide[i].Count == 0)
                {
                    // 1�}�X��̍��W��ݒ�
                    result.Add(pos + dire[i]);
                    continue;
                }
                // �Փː�̍��W��ݒ�
                result.Add(collide[i][collide[i].Count - 1] + dire[i]);
            }
        }
        else
        {
            // �e�����̃��X�g�����Z
            for (int i = 0; i < dire.Length; i++)
            {
                result.AddRange(collide[i]);
            }
        }

        return result.ToArray();
    }
    #endregion
}