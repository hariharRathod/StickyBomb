
using DG.Tweening;
using UnityEngine;

public class IncrementGateMovement : MonoBehaviour
{
	[SerializeField] private Transform startTransform, endTransform,gateTransform;
	[SerializeField] private float movementDuration;
	[SerializeField] private bool flip,gateShouldMove;

	private void Start()
	{
		if (!gateShouldMove) return;
		
		MoveGate();
	}


	private void MoveGate()
	{
		if(flip) 
		{
			MoveEndToStart();
			return;
		}
	
		MoveStartToEnd();
		
	}

	private void MoveStartToEnd()
	{
		gateTransform.localPosition = startTransform.localPosition;
		gateTransform.DOLocalMoveX(endTransform.localPosition.x, movementDuration).SetEase(Ease.Linear)
			.SetLoops(-1, LoopType.Yoyo);
	}

	private void MoveEndToStart()
	{
		gateTransform.localPosition = endTransform.localPosition;
		gateTransform.DOLocalMoveX(startTransform.localPosition.x, movementDuration).SetEase(Ease.Linear)
			.SetLoops(-1, LoopType.Yoyo);
	}

}
