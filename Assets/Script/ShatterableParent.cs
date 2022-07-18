using UnityEngine;

public class ShatterableParent : MonoBehaviour
{
	public bool isShattered, shouldUnparent, shouldOnlyBeShatteredByLastEnemy;
	[SerializeField] private Shatterable[] theShatterables;
	[SerializeField] private MeshRenderer overlapCube;
	[SerializeField] private bool shouldPlayBrickAudioOnShatter;
	[SerializeField] private int area;

	private bool makeSoundOnce;

	private void OnEnable()
	{
		EnemyEvents.EnemyDied += OnEnemyDied;
		EnemyEvents.GaintEnemyDied += OnGaintEnemyDied;
	}

	private void OnDisable()
	{
		EnemyEvents.EnemyDied -= OnEnemyDied;
		EnemyEvents.GaintEnemyDied -= OnGaintEnemyDied;
	}

	
	public void ShatterTheShatterables()
	{
		foreach (var shatterable in theShatterables)
		{
			shatterable.Shatter();
		}
		
		if(makeSoundOnce) return;

		makeSoundOnce = true;
		AudioManager.instance.Play("BrickBreakHigh");
		AudioManager.instance.Play("BrickFall" + Random.Range(1, 3));
	}

	private void OnEnemyDied(EnemyController obj)
	{
		if (area != LevelFlowController.only.currentArea) return;

		makeSoundOnce = false;
		
		foreach (var shatterable in theShatterables)
		{
			shatterable.MakeShatterable();
		}
	}
	
	private void OnGaintEnemyDied(EnemyGaintController obj)
	{
		if (area != LevelFlowController.only.currentArea) return;
		
		makeSoundOnce = false;
		
		foreach (var shatterable in theShatterables)
		{
			shatterable.MakeShatterable();
		}
	}
}
