
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFlowController : MonoBehaviour
{
	public static LevelFlowController only;
	public int currentArea;
	[SerializeField] public float maxRayDistance = 50f;
	
	[SerializeField] private List<int> enemiesInArea;
	public int enemiesInCurrentArea, enemiesKilledInCurrentArea;
	
	private readonly List<Transform> _deadBodies = new List<Transform>();
	[SerializeField] private int _totalEnemiesRemaining;

	private void Awake()
	{
		if (!only) only = this;
		else Destroy(gameObject);
	}

	private void OnEnable()
	{
		EnemyEvents.EnemyDied +=OnEnemyDied;
		EnemyEvents.GaintEnemyDied += OnGaintDied;
		GameEvents.MoveToNextArea += OnMoveNextArea;

	}

	private void OnDisable()
	{
		EnemyEvents.EnemyDied -=OnEnemyDied;
		EnemyEvents.GaintEnemyDied -= OnGaintDied;
		GameEvents.MoveToNextArea -= OnMoveNextArea;
		
	}

	private void Start()
	{
		if (enemiesInArea.Count == 0)
		{
			Debug.LogWarning("Level Flow Controller values not changed");
			Debug.Break();
		}

		enemiesInCurrentArea = enemiesInArea[currentArea];
		enemiesKilledInCurrentArea = 0;
		
		foreach (var area in enemiesInArea)
			_totalEnemiesRemaining += area;

	}
	
	
	public bool IsThisLastEnemy()
	{
		if (_totalEnemiesRemaining == 1) return true;

		return false;
	}
	
	public bool IsThisLastEnemyOfArea()
	{
		return enemiesInArea[currentArea] - enemiesKilledInCurrentArea == 1;
	}


	private void OnGaintDied(EnemyGaintController econ)
	{
		if(_deadBodies.Contains(econ.transform)) return;
		
		OnEnemyKilled();
		_deadBodies.Add(econ.transform);
	}

	private void OnEnemyDied(EnemyController econ)
	{
		if(_deadBodies.Contains(econ.transform)) return;
		
		OnEnemyKilled();
		_deadBodies.Add(econ.transform);
	}
	private void OnEnemyKilled()
	{
		enemiesKilledInCurrentArea++;
		_totalEnemiesRemaining--;
		if (enemiesKilledInCurrentArea >= enemiesInCurrentArea)
			StartCoroutine(WaitBeforeMovingToNextArea());
	}
	
	private IEnumerator WaitBeforeMovingToNextArea()
	{
		yield return new WaitForSeconds(1f);
		
		if (currentArea == enemiesInArea.Count - 1)
		{
			GameEvents.InvokeOnGameWin();
			
		}
		else
		{
			GameEvents.InvokeOnMoveToNextArea();
		}

	}
	
	private void OnMoveNextArea()
	{
		enemiesInCurrentArea = enemiesInArea[++currentArea];
		enemiesKilledInCurrentArea = 0;
	}
	
	
}
