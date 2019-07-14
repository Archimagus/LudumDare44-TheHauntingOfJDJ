using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Memory : ScriptableObject
{
	public AudioClip BaseMusic;
	public AudioClip WarpedMusic;
	public AudioClip VoiceOver;
	public Subtitle[] Subtitles;
	public Material[] PortraitSequence;


	[Header("Probably don't change these")]
	public GameEvent MemoryStartedEvent;
	public GameEvent MemoryLeftEvent;


	private EventData _eventData;
	private EventData EventData
	{
		get
		{
			if (_eventData == null)
				_eventData = new EventData { Data = new List<KVP> { new KVP(name, 0) } };
			return _eventData;
		}
	}

	public float Progress
	{
		get
		{
			if (MemoryObjects.Count > 0)
			{
				float valid = MemoryObjects.Count(m => !m.isActiveAndEnabled);
				return valid / MemoryObjects.Count;
			}
			return 0;
		}
	}
	public List<Carryable> MemoryObjects { get; } = new List<Carryable>();
	public event EventHandler<float> ProgressUpdated;

	public static Memory CurrentMemory { get; private set; }
	
	public void Enter()
	{
		if (CurrentMemory == null)
		{
			CurrentMemory = this;
			MemoryStartedEvent?.Raise(EventData);
		}
		else if (CurrentMemory == this)
			Leave();
	}
	public void Leave()
	{
		if (CurrentMemory == this)
		{
			CurrentMemory = null;
			MemoryLeftEvent?.Raise(EventData);
		}
	}
	public void ObjectDestroyed()
	{
		UpdateProgress();
	}
	public void UpdateProgress()
	{

		ProgressUpdated?.Invoke(this, Progress);
	}

}
