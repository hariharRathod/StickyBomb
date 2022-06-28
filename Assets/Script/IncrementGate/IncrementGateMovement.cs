
using DG.Tweening;
using UnityEngine;

public class IncrementGateMovement : MonoBehaviour
{
	[SerializeField] private Transform startTransform, endTransform,gateTransform;
	[SerializeField] private float movementDuration,moveStart,moveEnd;
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
		
		transform.position = new Vector3(moveStart,transform.position.y,transform.position.z);
		transform.DOMoveX(moveEnd, movementDuration).SetEase(Ease.Linear)
			.SetLoops(-1, LoopType.Yoyo);
	}

	private void MoveEndToStart()
	{
		transform.position= new Vector3(moveEnd,transform.position.y,transform.position.z);
		transform.DOMoveX(moveStart, movementDuration).SetEase(Ease.Linear)
			.SetLoops(-1, LoopType.Yoyo);
	}

}
