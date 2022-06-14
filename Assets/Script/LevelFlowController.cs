
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
	
	private void Awake()
	{
		if (!only) only = this;
		else Destroy(gameObject);
	}

	private void OnEnable()
	{
		EnemyEvents.EnemyDied +=OnEnemyDied;
		GameEvents.ReactNextArea += OnReachNextArea;
	}

	private void OnDisable()
	{
		EnemyEvents.EnemyDied -=OnEnemyDied;
		GameEvents.ReactNextArea -= OnReachNextArea;
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
		
		
	}
	
	public bool IsThisLastEnemyOfArea()
	{
		return enemiesInArea[currentArea] - enemiesKilledInCurrentArea == 1;
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
	
	private void OnReachNextArea()
	{
		enemiesInCurrentArea = enemiesInArea[++currentArea];
		enemiesKilledInCurrentArea = 0;
	}
	
	
}
