using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerEvents
{
	public static event Action<bool> CanShoot;

	public static void InvokeOnCanShoot(bool value)
	{
		CanShoot?.Invoke(value);
	}
}
