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

		// �����ʒu
		Vector3 startPos = transform.position;

		// �I���ʒu
		Vector3 endPos = startPos + new Vector3(0, 0, _moveAmount); // z������2.4f�i��

		// �������̂��߂̌o�H�쐬
		// z�����ɐi�ފԂ�y������0.5f�オ��A�܂�����y���W�ɖ߂�
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // �����ʒu
		waypoints[1] = startPos + new Vector3(0, _moveAmountY, _moveAmountHerf); // z����1.2f�i�݁Ay������0.5f�オ��
		waypoints[2] = endPos; // �ŏI�n�_��z����2.4f�i���y�����Ɍ��̍����ɖ߂�

		// DoTween���g���ăp�X��`��
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");

	}

	public void Koma_MoveBack()
	{

		// �����ʒu
		Vector3 startPos = transform.position;

		// �I���ʒu
		Vector3 endPos = startPos + new Vector3(0, 0, -_moveAmount); // z������2.4f�i��

		// �������̂��߂̌o�H�쐬
		// z�����ɐi�ފԂ�y������0.5f�オ��A�܂�����y���W�ɖ߂�
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // �����ʒu
		waypoints[1] = startPos + new Vector3(0, _moveAmountY, -_moveAmountHerf); // z����1.2f�i�݁Ay������0.5f�オ��
		waypoints[2] = endPos; // �ŏI�n�_��z����2.4f�i���y�����Ɍ��̍����ɖ߂�

		// DoTween���g���ăp�X��`��
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");

	}

	public void Koma_MoveRight()
	{

		// �����ʒu
		Vector3 startPos = transform.position;

		// �I���ʒu
		Vector3 endPos = startPos + new Vector3(_moveAmount, 0, 0); // x������2.4f�i��

		// �������̂��߂̌o�H�쐬
		// z�����ɐi�ފԂ�y������0.5f�オ��A�܂�����y���W�ɖ߂�
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // �����ʒu
		waypoints[1] = startPos + new Vector3(_moveAmountHerf, 0.5f, 0f); // x����1.2f�i�݁Ay������0.5f�オ��
		waypoints[2] = endPos; // �ŏI�n�_��x����2.4f�i���y�����Ɍ��̍����ɖ߂�

		// DoTween���g���ăp�X��`��
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");

	}

	public void Koma_MoveLeft()
	{

		// �����ʒu
		Vector3 startPos = transform.position;

		// �I���ʒu
		Vector3 endPos = startPos + new Vector3(-_moveAmount, 0, 0); // x������2.4f�i��

		// �������̂��߂̌o�H�쐬
		// z�����ɐi�ފԂ�y������0.5f�オ��A�܂�����y���W�ɖ߂�
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // �����ʒu
		waypoints[1] = startPos + new Vector3(-_moveAmountHerf, 0.5f, 0f); // x����1.2f�i�݁Ay������0.5f�オ��
		waypoints[2] = endPos; // �ŏI�n�_��x����2.4f�i���y�����Ɍ��̍����ɖ߂�

		// DoTween���g���ăp�X��`��
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");

	}

	public void Koma_MoveFrontRight()
	{

		// �����ʒu
		Vector3 startPos = transform.position;

		// �I���ʒu
		Vector3 endPos = startPos + new Vector3(_moveAmount, 0, _moveAmount); // x������2.4f�i��

		// �������̂��߂̌o�H�쐬
		// z�����ɐi�ފԂ�y������0.5f�オ��A�܂�����y���W�ɖ߂�
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // �����ʒu
		waypoints[1] = startPos + new Vector3(_moveAmountHerf, _moveAmountY, _moveAmount); // x����1.2f�i�݁Ay������0.5f�オ��
		waypoints[2] = endPos; // �ŏI�n�_��x����2.4f�i���y�����Ɍ��̍����ɖ߂�

		// DoTween���g���ăp�X��`��
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");

	}

	public void Koma_MoveFrontLeft()
    {
		// �����ʒu
		Vector3 startPos = transform.position;

		// �I���ʒu
		Vector3 endPos = startPos + new Vector3(-_moveAmount, 0, _moveAmount); // x������2.4f�i��

		// �������̂��߂̌o�H�쐬
		// z�����ɐi�ފԂ�y������0.5f�オ��A�܂�����y���W�ɖ߂�
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // �����ʒu
		waypoints[1] = startPos + new Vector3(-_moveAmountHerf, _moveAmountY, _moveAmount); // x����1.2f�i�݁Ay������0.5f�オ��
		waypoints[2] = endPos; // �ŏI�n�_��x����2.4f�i���y�����Ɍ��̍����ɖ߂�

		// DoTween���g���ăp�X��`��
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");
	}

	public void Koma_BackRight()
    {
		// �����ʒu
		Vector3 startPos = transform.position;

		// �I���ʒu
		Vector3 endPos = startPos + new Vector3(_moveAmount, 0, -_moveAmount); // x������2.4f�i��

		// �������̂��߂̌o�H�쐬
		// z�����ɐi�ފԂ�y������0.5f�オ��A�܂�����y���W�ɖ߂�
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // �����ʒu
		waypoints[1] = startPos + new Vector3(_moveAmountHerf, _moveAmountY, -_moveAmount); // x����1.2f�i�݁Ay������0.5f�オ��
		waypoints[2] = endPos; // �ŏI�n�_��x����2.4f�i���y�����Ɍ��̍����ɖ߂�

		// DoTween���g���ăp�X��`��
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");
	}

	public void Koma_BackLeft()
	{
		// �����ʒu
		Vector3 startPos = transform.position;

		// �I���ʒu
		Vector3 endPos = startPos + new Vector3(-_moveAmount, 0, -_moveAmount); // x������2.4f�i��

		// �������̂��߂̌o�H�쐬
		// z�����ɐi�ފԂ�y������0.5f�オ��A�܂�����y���W�ɖ߂�
		Vector3[] waypoints = new Vector3[3];
		waypoints[0] = startPos; // �����ʒu
		waypoints[1] = startPos + new Vector3(-_moveAmountHerf, _moveAmountY, -_moveAmount); // x����1.2f�i�݁Ay������0.5f�オ��
		waypoints[2] = endPos; // �ŏI�n�_��x����2.4f�i���y�����Ɍ��̍����ɖ߂�

		// DoTween���g���ăp�X��`��
		transform.DOPath(waypoints, 1f, PathType.CatmullRom).SetEase(Ease.OutQuad);

		anim.SetTrigger("move");
	}
}