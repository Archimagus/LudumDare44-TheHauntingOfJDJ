using System.Linq;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
	[SerializeField] private float _maxRange = 5f;

	Camera _camera;
	private void Start()
	{
		if (_camera == null)
		{
			_camera = GetComponentInChildren<Camera>();
		}
	}
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			var hits = Physics.RaycastAll(_camera.transform.position, _camera.transform.forward, _maxRange).OrderBy(h=>h.distance).ToArray();
			if (hits?.Any() ?? false)
			{
				foreach (var hit in hits)
				{
					if (hit.transform.IsChildOf(transform))
						continue;
					var interactable = hit.collider.GetComponentInParent<Interactable>();
					if (interactable?.CanInteract ?? false)
					{
						interactable.Interact();
					}
					// only care about the first object we hit that we are not carying.
					return;
				}
			}
		}
	}
}
