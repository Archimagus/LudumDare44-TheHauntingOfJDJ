using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Portrait : Interactable
{
	public Memory portraitMemory;

	[Header("Visuals")]
	public MeshRenderer portraitMesh;
	public int materialIndex = 1;
	public Image HilightImage;


	private void OnValidate()
	{
		if (portraitMesh == null)
			portraitMesh = GetComponentInChildren<MeshRenderer>();
		changeTexture(0);
	}
	private void Awake()
	{
		HilightImage.DOFade(0, 0);
		portraitMemory.ProgressUpdated += PortraitMemory_ProgressUpdated;
		changeTexture(0);
	}
	private void PortraitMemory_ProgressUpdated(object sender, float e)
	{
		int index = (int)((portraitMemory.PortraitSequence.Length-1) * e);
		changeTexture(index);
		if(e >= 1)
		{
			portraitMemory.Leave();
			CanInteract = false;
			HilightImage.DOFade(0, 0.5f);
		}
	}

	private void changeTexture(int index)
	{
		if ((portraitMemory?.PortraitSequence?.Length ?? 0) > index)
		{
			DOTween.Sequence().AppendInterval(5).AppendCallback(() =>
			{
				var mats = portraitMesh.sharedMaterials;
				mats[materialIndex] = portraitMemory.PortraitSequence[index];
				portraitMesh.sharedMaterials = mats;
			});
		}
	}

	public override void Interact()
	{
		base.Interact();
		portraitMemory.Enter();
		if(Memory.CurrentMemory == portraitMemory)
		{
			HilightImage.DOFade(0.24f, 2f);
		}
		else
		{
			HilightImage.DOFade(0, 0.5f);
		}
	}
	public void ObjectDestroyed()
	{
		portraitMemory.ObjectDestroyed();
	}
}
