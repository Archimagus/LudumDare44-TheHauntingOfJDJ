using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
	public SceneField MainMenuScene;
	public SceneField GameScene;
	public SceneField UiScene;
	public SceneField GameOverScene;

	public void LoadMainMenuScene()
	{
		SceneManager.LoadScene(MainMenuScene);
	}
	public void LoadGameScene()
	{
		SceneManager.LoadScene(GameScene);
	}
	public void LoadGameOverScene()
	{
		SceneManager.LoadScene(GameOverScene);
	}
	public void EnsureUISceneLoaded()
	{
		var uiScene = SceneManager.GetSceneByName(UiScene);
		if (uiScene.buildIndex == -1)
		{
			SceneManager.LoadScene(UiScene, LoadSceneMode.Additive);
		}
	}

	public void QuitGame()
	{
		if (Application.isEditor)
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}
		else
		{
			Application.Quit();
		}
	}

}
