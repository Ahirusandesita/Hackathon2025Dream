// --------------------------------------------------------- 
// King.cs 
// 
// CreateDay: 2025/1/18
// Creator  : Shizuku
// --------------------------------------------------------- 

using UnityEngine;
using System;

[RequireComponent(typeof(Koma))]
public class King : MonoBehaviour
{
	[SerializeField]
	private KomaController _komaCtrl = default;
	private Koma _myKing = default;

	public event Action OnDisappearKing;

	/// <summary>
	/// ‰Šú‰»ˆ—
	/// </summary>
    private void Awake()
    {
		_myKing = GetComponent<Koma>();

		GameManager gManager = FindAnyObjectByType<GameManager>();
		if(_myKing.MyPlayerNumber == PlayerNumber.Player1)
        {
			_komaCtrl = gManager.Player1.GetComponent<KomaController>();
        }
		else
        {
			_komaCtrl = gManager.Player2.GetComponent<KomaController>();
		}
    }

    /// <summary>
    /// <para>CheckKing</para>
    /// <para></para>
    /// </summary>
    private void CheckKing()
	{
		// ƒLƒ“ƒO‚ª‘¶İ‚µ‚È‚¢
		if (!_komaCtrl.OwnKomas.Contains(_myKing))
		{
			OnDisappearKing?.Invoke();
		}
	}
}