using System;


public class EnemyEvents
{
	public static event Action<EnemyController> EnemyDied;
	
	public static event Action<EnemyGaintController> GaintEnemyDied;

	public static event Action GaintBombCollison;

	public static void InvokeOnEnemyDied(EnemyController obj)
	{
		EnemyDied?.Invoke(obj);
	}

	public static void InvokeOnGaintEnemyDied(EnemyGaintController obj)
	{
		GaintEnemyDied?.Invoke(obj);
	}

	public static void InvokeOnGaintBombCollison()
	{
		GaintBombCollison?.Invoke();
	}
}
