using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
	public GameObject associatedKey;
	public bool isLocked = true;

	// Call this from interaction code when appropriate
	public void Interact()
	{
		// if player has key matching associatedKey then unlock
		Unlock();
	}

	private void Unlock()
	{
		// if player has key unlock
		isLocked = false;
		this.PlaySound(AudioClips.keys_unlock);
	}

}
