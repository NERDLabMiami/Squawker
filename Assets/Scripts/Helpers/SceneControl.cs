using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{


	public void ResetGame()
    {
		PlayerPrefs.DeleteAll();
    }

	public void loadSceneNumber(int level)
	{
		//		loadingScreen.SetActive(true);
		StartCoroutine(loadLevel(level));
	}

	IEnumerator loadLevel(int level)
	{
		AsyncOperation async = SceneManager.LoadSceneAsync(level);
		while (!async.isDone)
		{
			Debug.Log("ASYNC: " + async.progress);
			yield return async;
		}
		//	loadingScreen.SetActive (false);
		Debug.Log("Loading complete");
	}

}
