// --------------------------------------------------------- 
// KomaAnimation.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using DG.Tweening;

public class KomaAnimation : MonoBehaviour
{
	private Animator anim;

	private float _moveAmount = 2.4f;
	private float _moveAmountHerf	= 1.2f;

	private float _moveAmountY = 0.5f;

	private void Awake()
	{
		
	}
 
	private void Start ()
	{
		anim = GetComponent<Animator>();

		


	}

	 private void Update ()
	{

	}


	public void Koma_MoveFront()
    {

		// 初期位置
		Vector3 startPos = transform.position;

		// 終了位置
		Vector3 endPos = startPos + new Vector3(0, 0, _moveAmount); // z方向に2.4f進む

		// 放物線のための経路作成
		// z方向に進む間にy方向で0.5f上がり、また元のy座標に戻る
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // 初期位置
		waypoints[1] = startPos + new Vector3(0, _moveAmountY, _moveAmountHerf); // z方向1.2f進み、y方向に0.5f上がる
		waypoints[2] = endPos; // 最終地点でz方向2.4f進んでy方向に元の高さに戻る

		// DoTweenを使ってパスを描画
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");

	}

	public void Koma_MoveBack()
	{

		// 初期位置
		Vector3 startPos = transform.position;

		// 終了位置
		Vector3 endPos = startPos + new Vector3(0, 0, -_moveAmount); // z方向に2.4f進む

		// 放物線のための経路作成
		// z方向に進む間にy方向で0.5f上がり、また元のy座標に戻る
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // 初期位置
		waypoints[1] = startPos + new Vector3(0, _moveAmountY, -_moveAmountHerf); // z方向1.2f進み、y方向に0.5f上がる
		waypoints[2] = endPos; // 最終地点でz方向2.4f進んでy方向に元の高さに戻る

		// DoTweenを使ってパスを描画
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");

	}

	public void Koma_MoveRight()
	{

		// 初期位置
		Vector3 startPos = transform.position;

		// 終了位置
		Vector3 endPos = startPos + new Vector3(_moveAmount, 0, 0); // x方向に2.4f進む

		// 放物線のための経路作成
		// z方向に進む間にy方向で0.5f上がり、また元のy座標に戻る
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // 初期位置
		waypoints[1] = startPos + new Vector3(_moveAmountHerf, 0.5f, 0f); // x方向1.2f進み、y方向に0.5f上がる
		waypoints[2] = endPos; // 最終地点でx方向2.4f進んでy方向に元の高さに戻る

		// DoTweenを使ってパスを描画
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");

	}

	public void Koma_MoveLeft()
	{

		// 初期位置
		Vector3 startPos = transform.position;

		// 終了位置
		Vector3 endPos = startPos + new Vector3(-_moveAmount, 0, 0); // x方向に2.4f進む

		// 放物線のための経路作成
		// z方向に進む間にy方向で0.5f上がり、また元のy座標に戻る
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // 初期位置
		waypoints[1] = startPos + new Vector3(-_moveAmountHerf, 0.5f, 0f); // x方向1.2f進み、y方向に0.5f上がる
		waypoints[2] = endPos; // 最終地点でx方向2.4f進んでy方向に元の高さに戻る

		// DoTweenを使ってパスを描画
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");

	}

	public void Koma_MoveFrontRight()
	{

		// 初期位置
		Vector3 startPos = transform.position;

		// 終了位置
		Vector3 endPos = startPos + new Vector3(_moveAmount, 0, _moveAmount); // x方向に2.4f進む

		// 放物線のための経路作成
		// z方向に進む間にy方向で0.5f上がり、また元のy座標に戻る
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // 初期位置
		waypoints[1] = startPos + new Vector3(_moveAmountHerf, _moveAmountY, _moveAmount); // x方向1.2f進み、y方向に0.5f上がる
		waypoints[2] = endPos; // 最終地点でx方向2.4f進んでy方向に元の高さに戻る

		// DoTweenを使ってパスを描画
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");

	}

	public void Koma_MoveFrontLeft()
    {
		// 初期位置
		Vector3 startPos = transform.position;

		// 終了位置
		Vector3 endPos = startPos + new Vector3(-_moveAmount, 0, _moveAmount); // x方向に2.4f進む

		// 放物線のための経路作成
		// z方向に進む間にy方向で0.5f上がり、また元のy座標に戻る
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // 初期位置
		waypoints[1] = startPos + new Vector3(-_moveAmountHerf, _moveAmountY, _moveAmount); // x方向1.2f進み、y方向に0.5f上がる
		waypoints[2] = endPos; // 最終地点でx方向2.4f進んでy方向に元の高さに戻る

		// DoTweenを使ってパスを描画
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");
	}

	public void Koma_BackRight()
    {
		// 初期位置
		Vector3 startPos = transform.position;

		// 終了位置
		Vector3 endPos = startPos + new Vector3(_moveAmount, 0, -_moveAmount); // x方向に2.4f進む

		// 放物線のための経路作成
		// z方向に進む間にy方向で0.5f上がり、また元のy座標に戻る
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // 初期位置
		waypoints[1] = startPos + new Vector3(_moveAmountHerf, _moveAmountY, -_moveAmount); // x方向1.2f進み、y方向に0.5f上がる
		waypoints[2] = endPos; // 最終地点でx方向2.4f進んでy方向に元の高さに戻る

		// DoTweenを使ってパスを描画
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");
	}

	public void Koma_BackLeft()
	{
		// 初期位置
		Vector3 startPos = transform.position;

		// 終了位置
		Vector3 endPos = startPos + new Vector3(-_moveAmount, 0, -_moveAmount); // x方向に2.4f進む

		// 放物線のための経路作成
		// z方向に進む間にy方向で0.5f上がり、また元のy座標に戻る
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // 初期位置
		waypoints[1] = startPos + new Vector3(-_moveAmountHerf, _moveAmountY, -_moveAmount); // x方向1.2f進み、y方向に0.5f上がる
		waypoints[2] = endPos; // 最終地点でx方向2.4f進んでy方向に元の高さに戻る

		// DoTweenを使ってパスを描画
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");
	}
}