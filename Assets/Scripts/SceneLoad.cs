// --------------------------------------------------------- 
// SceneLoad.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
	private string SceneName = "Main";


	private void Awake()
	{

	}
 
	private void Start ()
	{

	}

	 private void Update ()
    {

    }

	public void MoveScene()
    {
		SceneManager.LoadScene(SceneName);
	}


}