using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CinematicManager : MonoBehaviour
{
	[SerializeField] Camera _introCamera;
	[SerializeField] Camera _outroCamera;
	[SerializeField] CinematicCameraRef[] _cameraReferences;
	GameObject _crossHair;
	GameObject _fire;
	Image _fader;
	private GameData _gamedata;
	Sequence _sequence;

	void Start()
	{
		_gamedata = Resources.Load<GameData>("GameData");
		_fader = GameObject.Find("Fader").GetComponent<Image>();
		_crossHair = GameObject.Find("CrossHair");
		_fire = GameObject.Find("Fire");

		runIntroSequence();
	}

#if UNITY_EDITOR
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha9))
			runGameOverSequence();
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			_sequence.Kill();
			_introCamera.gameObject.SetActive(false);
			_outroCamera.gameObject.SetActive(false);
			_fader.enabled = false;
			_crossHair.SetActive(true);
		}
	}
#endif

	public void QueueObjectDestroyedCinematic()
	{
		if (_cameraReferences.All(m => m.memory.Progress >= 1))
		{
			runGameOverSequence();
		}
		else
		{
			var cam = _cameraReferences.FirstOrDefault(c => c.memory == Memory.CurrentMemory);
			if (cam != null)
			{
				cam.camera.gameObject.SetActive(true);
			}
		}
	}

	private void runIntroSequence()
	{
		_introCamera.gameObject.SetActive(true);
		_fire.SetActive(false);
		_crossHair.SetActive(false);
		_fader.color = Color.black;
		_sequence = DOTween.Sequence();
		_sequence.AppendInterval(2);
		_sequence.Append(_fader.DOColor(new Color(0, 0, 0, 0), 2));
		_sequence.AppendCallback(() =>
		{
			_fire.SetActive(true);
			_fire.transform.localScale = new Vector3(1, 0, 1);
			_fire.transform.DOScaleY(1, 1);
			_fire.transform.DOMoveY(0.5f, 0);
			_fire.transform.DOMoveY(0.9f, 1);
		});
		_sequence.AppendInterval(20);
		_sequence.AppendCallback(() => { _fader.enabled = false; _crossHair.SetActive(true); });
	}
	private void runGameOverSequence()
	{
		_crossHair.SetActive(false);
		_fader.enabled = true;
		_fader.color = new Color(0, 0, 0, 0);
		_sequence = DOTween.Sequence();
		_sequence.AppendCallback(() => { _outroCamera.gameObject.SetActive(true); });
		_sequence.AppendInterval(2);
		_sequence.AppendCallback(() => { ShowSubtitles.Instance.ShowOutroSubtitles(); });
		_sequence.AppendInterval(10);
		_sequence.AppendCallback(() =>
		{
			_fire.transform.DOScaleY(0, 1);
			_fire.transform.DOMoveY(0.5f, 1);
		});
		_sequence.AppendInterval(1);
		_sequence.AppendCallback(() => { _fire.SetActive(false); });
		_sequence.AppendInterval(5);
		_sequence.AppendCallback(() => _fire.SetActive(false));
		_sequence.Append(_fader.DOColor(Color.black, 0));
		_sequence.AppendInterval(3);
		_sequence.AppendCallback(() => _gamedata.LoadGameOverScene());
	}
}

[System.Serializable]
public class CinematicCameraRef
{
	public Camera camera;
	public Memory memory;
}