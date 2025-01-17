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
    /// ����������
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
    /// <para>���ݒ肵�܂�</para>
    /// </summary>
    /// <param name="koma"></param>
    /// <param name="pos"></param>
    public void SetKoma(Koma koma, Vector2Int pos)
    {
        _ban[pos.y, pos.x] = koma;
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
        _ban[newPos.y, newPos.x] = temp;
        _ban[oldPos.y, oldPos.x] = null;
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
        // �ړ��\���W
        foreach(Vector2Int movable in movablePositions)
        {
            Vector2Int checkPos = pos + movable;

            // �ՊO
            if (!CheckPositionInBan(checkPos))
            {
                continue;
            }
            // �����Ȃ�
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
    /// <para>�U���\�ȃ}�X��Ԃ�</para>
    /// </summary>
    /// <param name="pos">���ݍ��W</param>
    /// <param name="movablePostions">�ړ��\�͈�</param>
    /// <returns>�ړ��\�ȃ}�X�i��΍��W�j</returns>
    public Vector2Int[] GetAttackablePosition(Vector2Int pos, Vector2Int[] movablePostions)
    {
        // �ԋp�p
        List<Vector2Int> poss = new List<Vector2Int>();
        PlayerNumber myTeam = _ban[pos.y, pos.x].MyPlayerNumber;

        // �ړ��\���W
        foreach (Vector2Int movable in movablePostions)
        {
            Vector2Int checkPos = pos + movable;

            // �ՊO
            if (!CheckPositionInBan(checkPos))
            {
                continue;
            }
            // �����Ȃ�
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