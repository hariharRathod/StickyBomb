using System;
using UnityEngine;

//ye class sirf test ke liye banaye,isse dhang ke jagah pe funtinality do.please....
public static class WeaponEvents
{
	public static event Action WeaponChangeEvent;
	public static event Action OnBombSelectEvent,OnBombExplosion,OnBombButtonPressed;
	public static event Action OnArrowSelectEvent,ArrowCollisionWithObjects,OnMultipleArrowCollison,OnArrowButtonPressed;

	public static event Action ArrowCollisonWithTargetDone;
	
	public static event Action<Transform,GameObject> ArrowRealeaseEvent;
	
	


	public static void InvokeWeaponActivate() => WeaponChangeEvent?.Invoke();
	public static void InvokeOnBombSelectEvent()=> OnBombSelectEvent?.Invoke();

	public static void InvokeOnArrowSelectEvent()=>OnArrowSelectEvent?.Invoke();

	public static void InvokeOnArrowRealeaseEvent(Transform t,GameObject obj)
	{
		ArrowRealeaseEvent?.Invoke(t,obj);
	}


	public static void InvokeArrowCollisonWithTargetDone()
	{
		ArrowCollisonWithTargetDone?.Invoke();
	}

	public static void InvokeArrowCollisionWithObjects()
	{
		ArrowCollisionWithObjects?.Invoke();
	}

	public static void InvokeOnBombExplosion()
	{
		OnBombExplosion?.Invoke();
	}

	public  static void InvokeOnMultipleArrowCollison()
	{
		OnMultipleArrowCollison?.Invoke();
	}

	public static void InvokeOnBombButtonPressed()
	{
		OnBombButtonPressed?.Invoke();
	}

	public static void InvokeOnArrowButtonPressed()
	{
		OnArrowButtonPressed?.Invoke();
	}
}
