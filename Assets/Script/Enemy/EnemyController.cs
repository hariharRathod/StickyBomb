using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
	private EnemyRefbank _my;
	private Transform _player;


	private List<GameObject> _bombsList;
	[SerializeField] private HealthCanvas healthCanvas;
	private float _health=1f;

	private void OnEnable()
	{
		GameEvents.TapToPlay += OnTapToPlay;
		GameEvents.ReactNextArea += OnReachNextArea;
	}

	private void OnDisable()
	{
		GameEvents.TapToPlay -= OnTapToPlay;
		GameEvents.ReactNextArea -= OnReachNextArea;
	}


	private void Start()
	{
		_bombsList=new List<GameObject>();
		_my = GetComponent<EnemyRefbank>();
		_player = GameObject.FindGameObjectWithTag("PlayerRoot").transform;
	}


	private void OnTapToPlay() => OnReachNextArea();
	
	public void DieFromBomb(bool getThrownBack)
	{
		if(_my.isDead) return;

		_my.isDead = true;
		_my.Movement.StopMovement();
		_my.Movement.DisableAgent();
		ResetHealthAfterDie();
			
		_my.RagdollController.GoRagdoll(getThrownBack);
		
		disableAllBombsOnMe();
		
		EnemyEvents.InvokeOnEnemyDied(this);
		//audio jab set honga on karna isee.......
		//AudioManager.instance.Play("Die");
	}
	private void StartChasingPlayer()
	{
		_my.Movement.SetChaseTarget(_player);
		_my.Movement.ChasePlayer(true);
	}
	private void OnReachNextArea()
	{
		if(_my.area != LevelFlowController.only.currentArea) return;
		print("Reach next area enemy");
		StartChasingPlayer();
		
		_my.RagdollController.UnKinematicise();
		
		
	}


	public void AddBomb(GameObject bomb)
	{
		_bombsList.Add(bomb);
	}

	public void disableAllBombsOnMe()
	{
		foreach (var bomb in _bombsList)
		{
			bomb.SetActive(false);
		}
	}


	public bool GetHit(float damage)
	{
		if (_my.isDead) return false;
			
		_health -= damage;
		healthCanvas.SetHealth(_health);
		_my.Animations.GetHit();
		
		if (_health > 0f)
		{
			return true;
		}

		DieFromHealth(true);
		_my.isDead = true;
		return false;

	}
	
	private void DieFromHealth(bool getThrownBack)
	{
		if(_my.isDead) return;

		_my.isDead = true;
		_my.Movement.StopMovement();
		_my.Movement.DisableAgent();
		ResetHealthAfterDie();
			
		_my.RagdollController.GoRagdoll(getThrownBack);
		disableAllBombsOnMe();
			
		EnemyEvents.InvokeOnEnemyDied(this);
		
	}

	public void ResetHealthAfterDie()
	{
		_health = 0f;
		healthCanvas.SetHealth(1f);
		healthCanvas.DisableCanvas();
	}




}
