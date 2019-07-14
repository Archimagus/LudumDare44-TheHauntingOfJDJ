using DG.Tweening;
using UnityEngine;


public class TogglePosition : MonoBehaviour
{
	[SerializeField] float _time;
	[SerializeField] Vector3 _targetPosition;
	[SerializeField] Vector3 _targetRotation;
	[SerializeField] AudioClip _openAudio;
	[SerializeField] AudioClip _closeAudio;


	bool open = false;

	Vector3 _initialPosition;
	Vector3 _initialRotation;
	// Start is called before the first frame update
	void Start()
	{
		_initialPosition = transform.localPosition;
		_initialRotation = transform.localRotation.eulerAngles;
	}

	public void Toggle()
	{
		open = !open;
		if(open)
		{
			if (_openAudio)
				this.PlaySound(_openAudio);

			transform.DOLocalMove(_targetPosition, _time).SetEase(Ease.InOutCubic);
			transform.DOLocalRotate(_targetRotation, _time).SetEase(Ease.InOutCubic);
		}
		else
		{
			if(_closeAudio)
				this.PlaySound(_closeAudio);

			transform.DOLocalMove(_initialPosition, _time).SetEase(Ease.InOutCubic);
			transform.DOLocalRotate(_initialPosition, _time).SetEase(Ease.InOutCubic);
		}
	}

	[ContextMenu("Record Target Position")]
	private void recordTarget()
	{
		_targetPosition = transform.localPosition ;
		_targetRotation = transform.localRotation.eulerAngles;
	}
}
