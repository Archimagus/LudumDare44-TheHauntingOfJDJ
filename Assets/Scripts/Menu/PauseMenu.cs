using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
	public GameObject PausePanel;
	private Vector3 _lastMousePosition;

	public static PauseMenu Instance { get; private set; }
	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
	}
	private void Update()
	{
		if (PausePanel.activeInHierarchy)
		{
			var es = EventSystem.current;
			if ((Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f && es.currentSelectedGameObject == null) ||
				(es.currentSelectedGameObject != null && es.currentSelectedGameObject.activeInHierarchy == false))
			{

				es.SetSelectedGameObject(GameObject.Find("ResumeButton").gameObject);
			}
			if (Input.mousePosition != _lastMousePosition)
			{
				es.SetSelectedGameObject(null);
			}
			_lastMousePosition = Input.mousePosition;
		}
	}
}
