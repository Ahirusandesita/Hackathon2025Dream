// --------------------------------------------------------- 
// ClickSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ClickSystem : MonoBehaviour
{
    #region variable 

    private float _rayRenge = 30f;

    private Vector3 _rayHitPotision;

    private Masu _hitPostionMasu;

    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
 {

 }
 
 private void Start ()
 {

 }

 private void Update ()
 {

        // ���N���b�N�����Ƃ�
        if (Input.GetMouseButton(0))
        {
            Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            Debug.DrawRay(ClickRay.origin, ClickRay.direction * _rayRenge, Color.green);

            // ���C�L���X�g����
            if (Physics.Raycast(ClickRay, out hit, _rayRenge))
            {
                _hitPostionMasu = hit.collider.GetComponent<Masu>();

                _rayHitPotision = hit.point;

                //Debug.Log("�����������@" + _rayHitPotision);   
            }
            else
            {
                return;
            }

        }
        #endregion
    }