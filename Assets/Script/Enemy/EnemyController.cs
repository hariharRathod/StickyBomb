using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
	private EnemyRefbank _my;
	private Transform _player;


	private List<GameObject> bombsList;

	private void OnEnable()
	{
		GameEvents.TapToPlay += OnTapToPlay;
	}

	private void OnDisable()
	{
		GameEvents.TapToPlay -= OnTapToPlay;
	}


	private void Start()
	{
		bombsList=new List<GameObject>();
		_my = GetComponent<EnemyRefbank>();
		_player = GameObject.FindGameObjectWithTag("PlayerRoot").transform;
	}


	private void OnTapToPlay() => OnReachNextArea();
	
	public void EnemyDie(bool getThrownBack)
	{
		if(_my.isDead) return;

		_my.isDead = true;
		_my.Movement.StopMovement();
		_my.Movement.DisableAgent();
		
			
		_my.RagdollController.GoRagdoll(getThrownBack);
		
		disableAllBombsOnMe();
			
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
			
		StartChasingPlayer();
		
		
	}


	public void AddBomb(GameObject bomb)
	{
		bombsList.Add(bomb);
	}

	public void disableAllBombsOnMe()
	{
		foreach (var bomb in bombsList)
		{
			bomb.SetActive(false);
		}
	}



}
