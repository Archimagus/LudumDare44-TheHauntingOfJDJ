using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
	[SerializeField] private EventData _eventData;
	[SerializeField] private GameEvent _interactEvent;
	[SerializeField] UnityEvent _onInteract;
	[SerializeField] protected bool _canInteract=true;

	public virtual bool CanInteract
	{
		get
		{
			return _canInteract;
		}
		set
		{
			_canInteract = value;
		}
	}

	public virtual void Interact()
	{
		_interactEvent?.Raise(_eventData);
		_onInteract?.Invoke();
	}
}
