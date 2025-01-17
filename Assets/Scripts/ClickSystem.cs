// --------------------------------------------------------- 
// ClickSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using System;
using UnityEngine;

public class ClickSystem : MonoBehaviour
{
    private float _rayRenge = 30f;
    private Vector3 _rayHitPotision;
    private Masu _hitPostionMasu;

    public event Action<Masu> OnClickMasu = default;

    private void Update()
    {
        // 左クリックしたとき
        if (Input.GetMouseButton(0))
        {
            Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            Debug.DrawRay(ClickRay.origin, ClickRay.direction * _rayRenge, Color.green);

            // レイキャスト発射
            if (Physics.Raycast(ClickRay, out hit, _rayRenge))
            {
                if (hit.collider.TryGetComponent(out Masu masu))
                {
                    OnClickMasu?.Invoke(masu);
                }

                _rayHitPotision = hit.point;

                //Debug.Log("当たった所　" + _rayHitPotision);   
            }
        }
    }
}