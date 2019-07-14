using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ShowSubtitles : MonoBehaviour
{
	TextMeshProUGUI _text;
	Image _background;
	public AudioClip IntroAudioClip;
	public Subtitle[] IntroSubtitles;

	public AudioClip OutroAudioClip;
	public Subtitle[] OutroSubtitles;

	public static ShowSubtitles Instance { get; private set; }

	private void Awake()
	{
		Instance = this;

		_text = GetComponentInChildren<TextMeshProUGUI>();
		_background = GetComponentInChildren<Image>();
		_text.text = string.Empty;

	}
	private void Start()
	{
		var sequence = DOTween.Sequence();
		sequence.AppendInterval(1).AppendCallback(() => showSubtitleSequence(IntroSubtitles, IntroAudioClip));	
	}

	HashSet<Memory> _playedVoiceOvers = new HashSet<Memory>();
	public void ShowMemorySubtitles()
	{
		if (Memory.CurrentMemory != null && _playedVoiceOvers.Add(Memory.CurrentMemory))
		{
			showSubtitleSequence(Memory.CurrentMemory.Subtitles, Memory.CurrentMemory.VoiceOver);
		}
	}
	public void ShowOutroSubtitles()
	{
		showSubtitleSequence(OutroSubtitles, OutroAudioClip);
	}
	public void HideSubtitles()
	{
		_text.text = string.Empty; _background.enabled = false;
	}

	private void showSubtitleSequence(Subtitle[] subtitles, AudioClip clip)
	{
		_background.enabled = true;
		var sequence = DOTween.Sequence();
		sequence.AppendCallback(() => this.PlaySound(clip));
		foreach (var t in subtitles)
		{
			sequence.AppendCallback(() => _text.text = t.Text).AppendInterval(t.DisplayTime);
		}
		sequence.AppendCallback(HideSubtitles);
	}
}
[System.Serializable]
public class Subtitle
{
	public string Text;
	public float DisplayTime;
}
