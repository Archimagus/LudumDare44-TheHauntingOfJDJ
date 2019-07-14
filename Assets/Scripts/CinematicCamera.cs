using DG.Tweening;
using UnityEngine;

public class CinematicCamera : MonoBehaviour
{
	public float InitialWaitTime = 2;
	public float MoveToTime = 2;
	public float WaitAtTargetTime = 2;
	public float MoveBackTime = 1;

	private Vector3 targetPos;
	private Quaternion targetRot;
	private Sequence sequence;

	private void OnEnable()
	{
		targetPos = transform.position;
		targetRot = transform.rotation;

		var player = Player.Instance;
		var cam = Camera.main;

		transform.position = cam.transform.position;
		transform.rotation = cam.transform.rotation;
		Player.Instance.gameObject.SetActive(false);

		sequence = DOTween.Sequence().AppendInterval(InitialWaitTime)
			.Append(transform.DOMove(targetPos, MoveToTime).SetEase(Ease.InOutCubic))
			.Join(transform.DORotateQuaternion(targetRot, MoveToTime).SetEase(Ease.InOutCubic))
			.AppendInterval(WaitAtTargetTime)
			.Append(transform.DOMove(cam.transform.position, MoveBackTime).SetEase(Ease.InOutCubic))
			.Join(transform.DORotateQuaternion(cam.transform.rotation, MoveBackTime).SetEase(Ease.InOutCubic))
			.AppendCallback(() =>
			{
				transform.position = targetPos;
				transform.rotation = targetRot;
				gameObject.SetActive(false);
				Player.Instance.gameObject.SetActive(true);
			});
	}
	private void OnDisable()
	{
		sequence.Kill();
		transform.position = targetPos;
		transform.rotation = targetRot;
		gameObject.SetActive(false);
		Player.Instance.gameObject.SetActive(true);
	}
}
