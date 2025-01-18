// --------------------------------------------------------- 
// KomaRebellionEffect.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;

public class KomaRebellionEffect : MonoBehaviour
{
    private bool isRotate = false;
    private bool isPosition = false;

    private bool isUp = false;
    private bool isDown = false;

    private float rotateValue = 0f;
    Vector3 rotation;
    private float positionValue = 0f;
    private Vector3 position;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isRotate)
        {
            transform.Rotate(new Vector3(0f, 180f * Time.deltaTime, 0f));
            rotateValue += 180f * Time.deltaTime;
            if (rotateValue >= 180f)
            {
                rotation.y += 180f;
                isRotate = false;
                transform.rotation = Quaternion.Euler(rotation);
            }
        }
        if (isPosition)
        {
            if (isUp)
            {
                transform.Translate(new Vector3(0f, 0.5f * Time.deltaTime, 0f));
                positionValue += 0.5f * Time.deltaTime;
                if (positionValue >= 0.5f)
                {
                    isUp = false;
                    isDown = true;
                }
            }
            if (isDown)
            {
                transform.Translate(new Vector3(0f, -0.5f * Time.deltaTime, 0f));
                positionValue -= 0.5f * Time.deltaTime;
            }
            if (positionValue <= 0f)
            {
                isDown = false;
                transform.position = position;
            }


        }
    }
    public void Rotation()
    {
        animator.SetTrigger("explosion");

        isRotate = true;
        isPosition = true;
        isUp = true;
        rotateValue = 0f;
        rotation = transform.rotation.eulerAngles;

        positionValue = 0f;
        position = transform.position;
    }

    private IEnumerator RotationYAnimation()
    {
        float rotateValue = 0f;
        Vector3 rotation = transform.rotation.eulerAngles;
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.01f);
        while (rotateValue < 180f)
        {
            transform.Rotate(new Vector3(0f, 2f, 0f));
            rotateValue += 2f;
            yield return waitForSeconds;
        }
        rotation.y += 180f;
        transform.rotation = Quaternion.Euler(rotation);
    }
    private IEnumerator PositionYAnimation()
    {
        float positionValue = 0f;
        Vector3 position = transform.position;
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.01f);
        while (positionValue < 0.5f)
        {
            transform.Translate(new Vector3(0f, 0.011f, 0f));
            positionValue += 0.011f;
            yield return waitForSeconds;
        }
        while (positionValue > 0f)
        {
            transform.Translate(new Vector3(0f, -0.011f, 0f));
            positionValue -= 0.011f;
            yield return waitForSeconds;
        }


        transform.position = position;
    }
}