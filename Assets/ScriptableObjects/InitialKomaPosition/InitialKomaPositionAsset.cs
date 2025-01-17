// --------------------------------------------------------- 
// InitialKomaPositionAsset.cs 
// 
// CreateDay: 
// Creator  : Takayanagi
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="NewInitialKomaPositionAsset", menuName ="ScriptableObject/InitialKomaPositionAsset")]
public class InitialKomaPositionAsset : ScriptableObject
{
	[SerializeField, Header("�e�v���C���[�̏�������΍��W�Őݒ�")]
	private List<KomaAssetPositionPair> _initialPositions = default;

	public IReadOnlyList<KomaAssetPositionPair> InitialPositions => _initialPositions;
}

[System.Serializable]
public class KomaAssetPositionPair
{
	[SerializeField]
	private KomaAsset _komaAsset = default;
	[SerializeField]
	private Vector2Int _position = default;

	public KomaAsset KomaAsset => _komaAsset;
	public Vector2Int Position => _position;
}