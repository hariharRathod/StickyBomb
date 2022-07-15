using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FloatingPropController : MonoBehaviour
{
	[SerializeField] private float yPosMultiplier,floatDuration;
	
	private Transform _transform;

	private void Start()
	{
		_transform=transform;
		MakePropFloat();
	}

	private void MakePropFloat()
	{
		_transform.DOMoveY( _transform.position.y * yPosMultiplier, floatDuration).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
	}

}
