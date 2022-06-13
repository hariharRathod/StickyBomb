using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvents
{
	public static event Action<EnemyController> EnemyDied;

	public static void InvokeOnEnemyDied(EnemyController obj)
	{
		EnemyDied?.Invoke(obj);
	}


	
}
