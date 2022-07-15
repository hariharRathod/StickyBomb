using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ContinousArrowShootTutorial : MonoBehaviour
{
	private void OnEnable()
	{
		GameEvents.TapToPlay += OnTapToPlay;
	}

	private void OnDisable()
	{
		GameEvents.TapToPlay -= OnTapToPlay;
	}

	private void Start()
	{
		gameObject.SetActive(false);

		DOVirtual.DelayedCall(0.2f, () => gameObject.SetActive(true));
	}
	
	private void OnTapToPlay()
	{
		transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));
	}

}
