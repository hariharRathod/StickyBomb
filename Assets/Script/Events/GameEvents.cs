
using System;
using UnityEngine;

public class GameEvents
{
	public static event Action TapToPlay;
	public static event Action GameLose;

	public static event Action MoveToNextArea;

	public static event Action GameWin;

	public static event Action ReactNextArea;


	public static void InvokeOnTapToPlay()
	{
		TapToPlay?.Invoke();
	}

	public static void InvokeOnGameLose()
	{
		GameLose?.Invoke();
	}

	public static void InvokeOnMoveToNextArea()
	{
		MoveToNextArea?.Invoke();
	}

	public static void InvokeOnGameWin()
	{
		GameWin?.Invoke();
	}

	public static void InvokeOnReactNextArea()
	{
		ReactNextArea?.Invoke();
	}
}
