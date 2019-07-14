using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioEvent : MonoBehaviour
{
	public float FadeTime = 1;

	public void SetFadeTime (float fadeTime)
	{
		FadeTime = fadeTime;
	}

	public void PlayMusic (AudioClip clip)
	{
		AudioManager.PlayMusic(clip, FadeTime);
	}

	public void PlayMusicSync(AudioClip clip)
	{
		AudioManager.PlayMusic(clip, FadeTime, true);
	}

	public void PlaySound (AudioClip clip)
	{
		AudioManager.PlaySound(this, clip);
	}

	HashSet<AudioClip> _playedVoiceOvers = new HashSet<AudioClip>();
	public void PlayMemoryVo(bool canRepeat)
	{
		if(_playedVoiceOvers.Add(Memory.CurrentMemory.VoiceOver) || canRepeat)
		{
			AudioManager.PlaySound(this, Memory.CurrentMemory.VoiceOver);
		}
	}
}
