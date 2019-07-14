using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private GameTime _gameTime;
	private MenuStack _menuStack;
	private GameData _gameData;

	public static GameManager Instance { get; private set; }
	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		
		_gameTime = Resources.Load<GameTime>("GameTime");
		if (_gameTime == null)
			Debug.LogError("GameTime not found");

		_menuStack = Resources.Load<MenuStack>("MenuStack");
		if (_menuStack == null)
			Debug.LogError("MenuStack not found");

		_gameData = Resources.Load<GameData>("GameData");
		if (_menuStack == null)
			Debug.LogError("GameData not found");

		_gameData.EnsureUISceneLoaded();
	}
	
	private void Update()
	{
		if (Input.GetButtonDown("Menu"))
		{
			if (_gameTime.MenuPause)
				_menuStack.CloseMenu();
			else
				_menuStack.OpenMenu(PauseMenu.Instance.PausePanel);
		}
	}
	public void QuitToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}