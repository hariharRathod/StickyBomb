using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemySideWalkController : MonoBehaviour
{
	[SerializeField] private float maxDist;
	private bool right, left=true;
	
	public void MoveSideOnAnimation()
	{
		if (left)
		{
			transform.DOMoveX(-3f, 0.4f).SetEase(Ease.Linear);
			right = true;
			left = false;
			return;
		}

		if (right)
		{
			transform.DOMoveX(3f, 0.4f).SetEase(Ease.Linear);
			left = true;
			return;
		}
	
		
	}
}
