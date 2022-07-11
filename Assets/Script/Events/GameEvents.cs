
using System;
using UnityEngine;

public class GameEvents
{
	public static event Action TapToPlay,FingerUp,BombRelease;
	public static event Action GameLose,GameWin;
	public static event Action MoveToNextArea,ReactNextArea,CurrentAreaAllEnemyKilled;
	public static event Action CameraFollowArrowStart,CircularViewStart,CircularViewEnd;

	public static event Action ContinousArrowShootEnable;
	
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
	public static void InvokeOnCameraFollowArrowStart()
	{
		CameraFollowArrowStart?.Invoke();
	}

	public static void InvokeOnFingerUp()
	{
		FingerUp?.Invoke();
	}

	public static void InvokeOnBombRelease()
	{
		BombRelease?.Invoke();
	}

	public static void InvokeOnCurrentAreaAllEnemyKilled()
	{
		CurrentAreaAllEnemyKilled?.Invoke();
	}

	public static void InvokeOnCircularViewStart()
	{
		CircularViewStart?.Invoke();
	}

	public static void InvokeOnCircularViewEnd()
	{
		CircularViewEnd?.Invoke();
	}

	public static void InvokeOnContinousArrowShootEnable()
	{
		ContinousArrowShootEnable?.Invoke();
	}
}
