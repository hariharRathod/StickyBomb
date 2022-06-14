using System;


public class EnemyEvents
{
	public static event Action<EnemyController> EnemyDied;

	public static void InvokeOnEnemyDied(EnemyController obj)
	{
		EnemyDied?.Invoke(obj);
	}


	
}
