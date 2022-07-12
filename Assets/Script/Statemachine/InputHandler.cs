using System;
using DG.Tweening;
using UnityEngine;


public enum InputState { Idle, DragToAim,TapState,AimingState,Disabled,ContinousShootState }
public class InputHandler : MonoBehaviour
{
	private static InputStateBase _currentInputState;
		
	//all states
	private static readonly IdleState IdleState = new IdleState();
	private static readonly DragToAimState DragToAimState =  new DragToAimState();
	private static readonly DisabledState DisabledState =  new DisabledState();
	private static readonly TapState TapState=new TapState();
	private static ContinousShootState _continousShootState;
	private static AimingState _aimingState;
	
	
	private bool _hasTappedToPlay;
	private static bool _inCoolDown;
	private GameObject _player;
	private PlayerRefBank _playerRefBank;

	private void OnEnable()
	{
		GameEvents.TapToPlay += OnTapToPlay;
		GameEvents.GameLose += OnGameEnd;
		GameEvents.GameWin += OnGameWin;
		GameEvents.CameraFollowArrowStart += OnCameraFollowArrowStart;
		GameEvents.MoveToNextArea += OnMoveNextArea;
		GameEvents.ReactNextArea += OnReachNextArea;
		GameEvents.CircularViewStart += OnCircularViewStart;
		GameEvents.CircularViewEnd += OnCircularViewEnd;
	}

	
	private void OnDisable()
	{
		GameEvents.TapToPlay -= OnTapToPlay;
		GameEvents.GameLose -= OnGameEnd;
		GameEvents.GameWin -= OnGameWin;
		GameEvents.CameraFollowArrowStart -= OnCameraFollowArrowStart;
		GameEvents.MoveToNextArea -= OnMoveNextArea;
		GameEvents.ReactNextArea -= OnReachNextArea;
		GameEvents.CircularViewStart -= OnCircularViewStart;
		GameEvents.CircularViewEnd -= OnCircularViewEnd;
		
	}


	private void Start()
	{
		InputExtensions.IsUsingTouch = Application.platform != RuntimePlatform.WindowsEditor &&
									   (Application.platform == RuntimePlatform.Android ||
										Application.platform == RuntimePlatform.IPhonePlayer);
		InputExtensions.TouchInputDivisor = MyHelpers.RemapClamped(1920, 2400, 30, 20, Screen.height);
		_player = GameObject.FindGameObjectWithTag("PlayerRoot");
		_playerRefBank = _player.GetComponent<PlayerRefBank>();
		_ = new InputStateBase(_playerRefBank);
		_aimingState=new AimingState(_player.GetComponent<AimController>());
		_continousShootState = new ContinousShootState(_player.GetComponent<AimController>());
		if (LevelFlowController.only.ContinousArrowEnable) return;
		
		_currentInputState = IdleState;
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

		if (!LevelFlowController.only.ContinousArrowEnable) return;

		if (!InputExtensions.GetFingerUp()) return;
		
		if(_playerRefBank.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Arrow)
		{
			print("Arrow activate");
			DOVirtual.DelayedCall(0.2f,()=>_playerRefBank.ArrowShoot.ActivateArrow());
		}

	}

	private InputStateBase HandleInput()
	{
		//if (InputExtensions.GetFingerUp()) return TapState;
		if(LevelFlowController.only.ContinousArrowEnable)
			if (InputExtensions.GetFingerHeld())
				return _continousShootState;

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
			InputState.ContinousShootState=>_continousShootState,
			_ => throw new ArgumentOutOfRangeException(nameof(state), state, "aisa kya pass kar diya vrooo tune yahaan")
		};


		print("currentInputstate: " + _currentInputState);
		_currentInputState?.OnEnter();
	}
	private void OnTapToPlay()
	{
		_hasTappedToPlay = true;
		if(LevelFlowController.only.ContinousArrowEnable)
			AssignNewState(InputState.ContinousShootState);
	}

	private static void OnGameEnd() => AssignNewState(InputState.Disabled);

	public static void PutInCoolDown()
	{
		if (LevelFlowController.only.ContinousArrowEnable) return;
		
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
		if (LevelFlowController.only.ContinousArrowEnable)
		{
			AssignNewState(InputState.ContinousShootState); 
			return;
		}
		
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

	
	private void OnCircularViewStart()
	{
		AssignNewState(InputState.Disabled);
	}
	
	private void OnCircularViewEnd()
	{
		AssignNewState(InputState.Idle);
	}
	
}
