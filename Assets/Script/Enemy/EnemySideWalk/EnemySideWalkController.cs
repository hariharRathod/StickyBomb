using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemySideWalkController : MonoBehaviour
{
	[SerializeField] private float maxDist,leftPos,rightPos;
	private bool right, left=true;
	
	public void MoveSideOnAnimation()
	{
		if (left)
		{
			transform.DOMoveX(leftPos, 1.4f).SetEase(Ease.Linear);
			right = true;
			left = false;
			return;
		}

		if (right)
		{
			transform.DOMoveX(rightPos, 1.4f).SetEase(Ease.Linear);
			left = true;
			return;
		}
	
		
	}
}
