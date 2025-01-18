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

	[SerializeField]
	AudioSource audio;

	[SerializeField]
	AudioClip clip;


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
		audio.PlayOneShot(clip);

		SceneManager.LoadScene(SceneName);
	}


}