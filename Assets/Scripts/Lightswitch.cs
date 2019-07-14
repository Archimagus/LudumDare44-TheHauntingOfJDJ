using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
	public GameObject Switch;

	private Quaternion _onRotationValue = new Quaternion(0, 90,-45f, 0);
	private Quaternion _offRotationValue = new Quaternion(0, 90,-135f, 0);

	[SerializeField] private float _onLightIntensity = 0.0f;
	[SerializeField] bool _isOn = true;

	[SerializeField] List<GameObject> _sconces;

	[SerializeField] private Material _onMaterial;
	[SerializeField] private Material _offMaterial;

	private MeshRenderer _currentRenderer;

	void Awake()
    {
		// Set on intensity to whatever we currently have the lights set as in the scene
		// This way if we change it to be brighter this will just work.
		_onLightIntensity = _sconces[0].GetComponentInChildren<Light>().intensity;

		if (Switch.transform.localRotation == _onRotationValue)
		{
			_isOn = true;
		}
		else
		{
			_isOn = false;
		}

	}

	public void FlipSwitch()
	{
		if(_isOn)
		{
			TurnOff();
		}
		else
		{
			TurnOn();
		}
	}

	private void TurnOn()
	{
		Switch.transform.localRotation = _onRotationValue;

		foreach (GameObject sconce in _sconces)
		{
			sconce.GetComponentInChildren<Light>().intensity = _onLightIntensity;
			_currentRenderer = sconce.GetComponent<MeshRenderer>();
			_currentRenderer.sharedMaterials[2] = _onMaterial;
			this.PlaySound(AudioClips.light_switch_on);
		}

		_isOn = true;
	}

	private void TurnOff()
	{
		Switch.transform.localRotation = _offRotationValue;

		foreach (GameObject sconce in _sconces)
		{
			sconce.GetComponentInChildren<Light>().intensity = 0.0f;
			_currentRenderer = sconce.GetComponent<MeshRenderer>();
			_currentRenderer.sharedMaterials[2] = _offMaterial;
			this.PlaySound(AudioClips.light_switch_off);
		}

		_isOn = false;
	}
}
