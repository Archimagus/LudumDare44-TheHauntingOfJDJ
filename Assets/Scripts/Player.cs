
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Transform CarryPoint;
	public static Player Instance { get; private set; }
	private void OnEnable()
	{
		Instance = this;
	}
	public bool CarryingObject { get { return CarriedObject != null; } }
	public Carryable CarriedObject { get; private set; }

	public void AttachObject(Carryable o)
	{
		CarriedObject = o;
		var t = o.transform;
		t.SetParent(CarryPoint, true);
		t.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InOutCubic);
		t.DOLocalRotate(Vector3.zero, 0.5f);
	}
	public void Detach()
	{
		CarryPoint.DetachChildren();
		CarriedObject = null;
	}
}
