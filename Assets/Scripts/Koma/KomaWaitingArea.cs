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
    private List<GameObject> waitingAreas = new List<GameObject>();
    private List<POWInfo> canPuts = new List<POWInfo>();
    private class POWInfo
    {
        public bool canPuts;
        public Koma koma;
        public POWInfo(bool canPuts)
        {
            this.canPuts = canPuts;
        }
    }
    public void InjectWaitingTransform(List<GameObject> waitingAreas)
    {
        this.waitingAreas = waitingAreas;

        for (int i = 0; i < canPuts.Count; i++)
        {
            canPuts.Add(new POWInfo(true));
        }
    }

    public void Arrangement(Koma koma)
    {

        for (int i = 0; i < canPuts.Count; i++)
        {
            if (canPuts[i].canPuts)
            {
                koma.transform.position = waitingAreas[i].transform.position;
                canPuts[i].canPuts = false;
                canPuts[i].koma = koma;
                return;
            }
        }
        koma.transform.position = Vector3.zero;
        //waitingKomas.Add(koma);
    }

    public void Remove(Koma koma)
    {
        for(int i = 0; i < canPuts.Count; i++)
        {
            if(canPuts[i].koma == koma)
            {
                canPuts[i].koma = null;
                canPuts[i].canPuts = true;
                return;
            }
        }
    }
}