using System;
using UnityEngine;

//ye class sirf test ke liye banaye,isse dhang ke jagah pe funtinality do.please....
public static class WeaponEvents
{
	public static event Action WeaponChangeEvent;
	public static event Action OnBombSelectEvent;
	public static event Action OnArrowSelectEvent;
	public static void InvokeWeaponActivate() => WeaponChangeEvent?.Invoke();
	public static void InvokeOnBombSelectEvent()=> OnBombSelectEvent?.Invoke();

	public static void InvokeOnArrowSelectEvent()=>OnArrowSelectEvent?.Invoke();
	
}
