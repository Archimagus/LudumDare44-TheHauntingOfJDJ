using UnityEngine;

public class Carryable : Interactable
{
	[SerializeField] private Memory _associatedMemory;

	private void Awake()
	{
		if(_associatedMemory != null)
		{
			_associatedMemory.MemoryObjects.Add(this);
		}
	}

	public override bool CanInteract
	{
		get
		{
			return base.CanInteract
				&& Player.Instance.CarryingObject == false
				&& (_associatedMemory == null || _associatedMemory == Memory.CurrentMemory);
		}

		set
		{
			base.CanInteract = value;
		}
	}
	public override void Interact()
	{
		base.Interact();
		Player.Instance.AttachObject(this);
		this.PlaySound(AudioClips.item_pick_up);
	}
}
