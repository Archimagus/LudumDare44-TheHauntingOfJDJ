using DG.Tweening;
using UnityEngine;

public class ConsumeObject : MonoBehaviour
{
	[SerializeField]private GameEvent _objectConsumedEvent=null;
	public void ConsumeCarriedObject()
	{
		if(Player.Instance.CarryingObject)
		{
			var obj = Player.Instance.CarriedObject;
			Player.Instance.Detach();
			DOTween.Sequence().Append(obj.transform.DOMove(transform.position, 0.5f).SetEase(Ease.InBack)).AppendCallback(() =>
			{
				obj.gameObject.SetActive(false);
				_objectConsumedEvent.Raise(new EventData { Data = new System.Collections.Generic.List<KVP> { new KVP(obj.name, 0) } });
			});

			this.PlaySound(AudioClips.fire_burst, SoundType.Ambience, this.transform.position);
		}
	}
}
