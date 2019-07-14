using DG.Tweening;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	[SerializeField] AudioClip _basicMusic = null;
	[SerializeField] AudioClip _warpedMusic = null;
	[SerializeField]
	[Tooltip("Seconts before return, 0 for permanent change")]
	float _duration = 0;
	Sequence _sequence;
	private void Start()
	{
		if (AudioManager.CurrentMusic != _basicMusic)
			AudioManager.PlayMusic(_basicMusic);
	}
	public void Swap()
	{

		if ((_sequence?.active ?? false) == false)
		{
			_sequence = DOTween.Sequence();
			_sequence.SetUpdate(true);
			_sequence.AppendCallback(() => AudioManager.PlayMusic(Memory.CurrentMemory?.WarpedMusic ?? _warpedMusic, 1, true));
			if (_duration > 0)
			{
				_sequence.AppendInterval(_duration);
				_sequence.AppendCallback(() => AudioManager.PlayMusic(Memory.CurrentMemory?.BaseMusic ?? _basicMusic, 1, true));
			}
		}
	}
	public void EnterMemory(DataVariable data)
	{
		AudioManager.PlayMusic(Memory.CurrentMemory.BaseMusic, 1, true);
	}
	public void LeaveMemory(DataVariable data)
	{
		AudioManager.PlayMusic(_basicMusic, 1, true);
	}
}
