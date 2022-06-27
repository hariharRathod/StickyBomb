using System;
using DG.Tweening;
using UnityEngine;


public enum InputState { Idle, DragToAim,TapState,AimingState,Disabled }
public class InputHandler : MonoBehaviour
{
	private static InputStateBase _currentInputState;
		
	//all states
	private static readonly IdleState IdleState = new IdleState();
	private static readonly DragToAimState DragToAimState =  new DragToAimState();
	private static readonly DisabledState DisabledState =  new DisabledState();
	private static readonly TapState TapState=new TapState();
	private static AimingState _aimingState;
	
	
	private bool _hasTappedToPlay;
	private static bool _inCoolDown;

	private void OnEnable()
	{
		GameEvents.TapToPlay += OnTapToPlay;
		GameEvents.GameLose += OnGameEnd;
		GameEvents.GameWin += OnGameWin;
		GameEvents.CameraFollowArrowStart += OnCameraFollowArrowStart;
		GameEvents.MoveToNextArea += OnMoveNextArea;
		GameEvents.ReactNextArea += OnReachNextArea;
	}

	
	private void OnDisable()
	{
		GameEvents.TapToPlay -= OnTapToPlay;
		GameEvents.GameLose -= OnGameEnd;
		GameEvents.GameWin -= OnGameWin;
		GameEvents.CameraFollowArrowStart -= OnCameraFollowArrowStart;
		GameEvents.MoveToNextArea -= OnMoveNextArea;
		GameEvents.ReactNextArea -= OnReachNextArea;
	}

	private void Start()
	{
		_currentInputState = IdleState;
		InputExtensions.IsUsingTouch = Application.platform != RuntimePlatform.WindowsEditor &&
									   (Application.platform == RuntimePlatform.Android ||
										Application.platform == RuntimePlatform.IPhonePlayer);
		InputExtensions.TouchInputDivisor = MyHelpers.RemapClamped(1920, 2400, 30, 20, Screen.height);
		var player = GameObject.FindGameObjectWithTag("PlayerRoot");
		var refbank = player.GetComponent<PlayerRefBank>();
		_ = new InputStateBase(refbank);
		_aimingState=new AimingState(player.GetComponent<AimController>());
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
		//if (InputExtensions.GetFingerUp()) return TapState;

		if (InputExtensions.GetFingerHeld()) return _aimingState;
		
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
			InputState.TapState=>TapState,
			InputState.AimingState=>_aimingState,
			_ => throw new ArgumentOutOfRangeException(nameof(state), state, "aisa kya pass kar diya vrooo tune yahaan")
		};

		_currentInputState?.OnEnter();
	}
	private void OnTapToPlay() => _hasTappedToPlay = true;
	
	private static void OnGameEnd() => AssignNewState(InputState.Disabled);

	public static void PutInCoolDown()
	{
		AssignNewState(InputState.Disabled);
		DOVirtual.DelayedCall(0.3f, TapCoolDown);

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
	
	private void OnMoveNextArea()
	{
		AssignNewState(InputState.Disabled);
	}
	
	private void OnReachNextArea()
	{
		AssignNewState(InputState.Idle);
	}

	public static void ScreenShakeBegin()
	{
		AssignNewState(InputState.Disabled);
	}

	public static void ScreenShakeEnd()
	{
		AssignNewState(InputState.Idle);
	}


}
