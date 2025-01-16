// --------------------------------------------------------- 
// POWTest.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class POWTest : MonoBehaviour
{
	[SerializeField]
	private List<Koma> komas_Player1 = new List<Koma>();
	[SerializeField]
	private List<Koma> komas_Player2 = new List<Koma>();

	private void Awake()
	{

	}
 
	private void Start ()
	{
		GameManager gameManager = FindObjectOfType<GameManager>();
		foreach(Koma item in komas_Player1)
        {
			//gameManager.Player1.GetComponent<KomaController>().Komas.Add(item);
        }
		foreach (Koma item in komas_Player2)
		{
			//gameManager.Player2.GetComponent<KomaController>().Komas.Add(item);
		}
	}

	 private void Update ()
	{

	}

}