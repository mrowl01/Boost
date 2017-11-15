using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBoss : MonoBehaviour 
{

	public void LoadNextScene()
	{
		Scene currentScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (currentScene.buildIndex + 1);
	}
	public void Lose ()
	{
		SceneManager.LoadScene ("Lose");
	}
	public void Win()
	{
		SceneManager.LoadScene ("Win");
	}
	public void LoadScene(string levelName)
	{
		SceneManager.LoadScene (levelName);
	}
}
