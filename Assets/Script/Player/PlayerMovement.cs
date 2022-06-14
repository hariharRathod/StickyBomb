
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private bool followOnStart;
	private SplineFollower _spline;

	private void OnEnable()
	{
		GameEvents.MoveToNextArea += OnMoveToNextArea;
		GameEvents.ReactNextArea += OnReachNextArea;
	}

	private void OnDisable()
	{
		GameEvents.MoveToNextArea -= OnMoveToNextArea;
		GameEvents.ReactNextArea -= OnReachNextArea;
	}

	private void Start()
	{
		_spline = GetComponent<SplineFollower>();
		
		//if(_spline.spline) return;
		
	}
	
	private void OnMoveToNextArea()
	{
		DOVirtual.DelayedCall(1.2f, StartFollowing);
	}

	private void StartFollowing()
	{
		_spline.follow = true;
	}

	private void OnReachNextArea()
	{
		//StopFollowing
		_spline.follow = false;
	}
}
