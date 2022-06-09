using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ye class sirf test ke liye banaye,isse dhang ke jagah pe funtinality do.please....
public static class WeaponEvents
{
	public static event Action WeaponActivateEvent;

	public static void InvokeWeaponActivate() => WeaponActivateEvent?.Invoke();
}
