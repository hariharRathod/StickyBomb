
using System;
using UnityEngine;

public class GameEvents
{
	public static event Action TapToPlay;
	public static event Action GameLose;


	public static void InvokeOnTapToPlay()
	{
		TapToPlay?.Invoke();
	}

	public static void InvokeOnGameLose()
	{
		GameLose?.Invoke();
	}
}
