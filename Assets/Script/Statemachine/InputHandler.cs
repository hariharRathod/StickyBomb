using System;
using UnityEngine;


public enum InputState { Idle, DragToAim }
public class InputHandler : MonoBehaviour
{
	private static InputStateBase _currentInputState;
		
	//all states
	private static readonly IdleState IdleState = new IdleState();
	private static readonly DragToAimState DragToAimState =  new DragToAimState();
	
	
	private bool _hasTappedToPlay;

	private void Start()
	{
		_currentInputState = IdleState;
		InputExtensions.IsUsingTouch = false;
		var player = GameObject.FindGameObjectWithTag("PlayerRoot");
		var refbank = player.GetComponent<PlayerRefBank>();
		_ = new InputStateBase(refbank);
		OnTapToPlay();
	}


	private void Update()
	{
		//if (!_hasTappedToPlay) return;

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
			return DragToAimState;

		return _currentInputState;
	}
	
	
	public static void AssignNewState(InputState state)
	{
		_currentInputState?.OnExit();

		_currentInputState = state switch
		{
			InputState.Idle => IdleState, 
			InputState.DragToAim=> DragToAimState,
			_ => throw new ArgumentOutOfRangeException(nameof(state), state, "aisa kya pass kar diya vrooo tune yahaan")
		};

		_currentInputState?.OnEnter();
	}
	private void OnTapToPlay() => _hasTappedToPlay = true;
}
