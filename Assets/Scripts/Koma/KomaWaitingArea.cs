// --------------------------------------------------------- 
// KomaWaitingArea.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KomaWaitingArea : MonoBehaviour
{
    private List<Koma> waitingKomas = new List<Koma>();
    private Transform waitingTransform;

    public void InjectWaitingTransform(Transform transform)
    {
        this.waitingTransform = transform;
    }

    public void Arrangement(Koma koma)
    {
        //waitingKomas.Add(koma);
        koma.transform.position = waitingTransform.position;
    }

}