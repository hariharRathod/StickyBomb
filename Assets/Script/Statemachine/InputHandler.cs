using System;
using DG.Tweening;
using UnityEngine;


public enum InputState { Idle, DragToAim,Disabled }
public class InputHandler : MonoBehaviour
{
	private static InputStateBase _currentInputState;
		
	//all states
	private static readonly IdleState IdleState = new IdleState();
	private static readonly DragToAimState DragToAimState =  new DragToAimState();
	private static readonly DisabledState DisabledState =  new DisabledState();
	
	
	private bool _hasTappedToPlay;
	private static bool _inCoolDown;

	private void OnEnable()
	{
		GameEvents.TapToPlay += OnTapToPlay;
		GameEvents.GameLose += OnGameEnd;
		GameEvents.GameWin += OnGameWin;
		GameEvents.CameraFollowArrowStart += OnCameraFollowArrowStart;
	}

	
	private void OnDisable()
	{
		GameEvents.TapToPlay -= OnTapToPlay;
		GameEvents.GameLose -= OnGameEnd;
		GameEvents.GameWin -= OnGameWin;
		GameEvents.CameraFollowArrowStart -= OnCameraFollowArrowStart;
	}

	
	private void Start()
	{
		_currentInputState = IdleState;
		InputExtensions.IsUsingTouch = false;
		var player = GameObject.FindGameObjectWithTag("PlayerRoot");
		var refbank = player.GetComponent<PlayerRefBank>();
		_ = new InputStateBase(refbank);
		//OnTapToPlay();//yaha ye kya karra hai saleeeeeeee
		
	}


	private void Update()
	{
		if (!_hasTappedToPlay) return;

		if (_inCoolDown) return;

		if (_currentInputState is IdleState)
		{
			_currentInputState = HandleInput();
			_currentInputState?.OnEnter();
		}

		_currentInputState?.Execute();


	}

	private InputStateBase HandleInput()
	{
		if (InputExtensions.GetFingerDown())
		{
			//abhi ke liye taptoplay yaha invoke karra hu hat dena isseee..........
			// abhi ke liye comment kiya hai sirf puri taraf hata saleeeeeeee
			//GameEvents.InvokeOnTapToPlay();
			return DragToAimState;
		}

		

		return _currentInputState;
	}
	
	
	public static void AssignNewState(InputState state)
	{
		_currentInputState?.OnExit();

		_currentInputState = state switch
		{
			InputState.Idle => IdleState, 
			InputState.DragToAim=> DragToAimState,
			InputState.Disabled=>DisabledState,
			_ => throw new ArgumentOutOfRangeException(nameof(state), state, "aisa kya pass kar diya vrooo tune yahaan")
		};

		_currentInputState?.OnEnter();
	}
	private void OnTapToPlay() => _hasTappedToPlay = true;
	
	private static void OnGameEnd() => AssignNewState(InputState.Disabled);

	public static void PutInCoolDown()
	{
		AssignNewState(InputState.Disabled);
		DOVirtual.DelayedCall(0.25f, TapCoolDown);

	}

	private static void TapCoolDown()
	{
		AssignNewState(InputState.Idle);
		_inCoolDown = false;
	}

	private void OnCameraFollowArrowStart()
	{
		AssignNewState(InputState.Disabled);
	}

	
	private void OnGameWin()
	{
		AssignNewState(InputState.Disabled);
	}
}
