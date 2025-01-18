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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
           // Rotation();
        }
    }
    public void Rotation()
    {
        StartCoroutine(RotationYAnimation());
        StartCoroutine(PositionYAnimation());
    }
    private IEnumerator RotationYAnimation()
    {
        float rotateValue = 0f;
        Vector3 rotation = transform.rotation.eulerAngles;
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.01f);
        while(rotateValue < 180f)
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