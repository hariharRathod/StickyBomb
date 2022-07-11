using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyController : MonoBehaviour,IStickable,IExplodDamageable
{
	private EnemyRefbank _my;
	private Transform _player;

	private List<GameObject> _bombsList;
	[SerializeField] private HealthCanvas healthCanvas;
	private float _health=1f;

	//bro zyada complicated nahi hogaya ye?,pucho apne aap se ye........................
	[SerializeField] private IStickable.StickableBehaviour stickingBehaviour;
	[SerializeField] private IExplodDamageable.ExplodableBehaviour explodBehaviour;

	[SerializeField] private bool isEnemyShielded,isEnemySideWalk,isEnemyHostageHold,ShouldEnemySideWalkOneShotRagdoll;
	
	
	
	//socaho kya me sahi karra hu ye
	public float Health
	{
		get => _health;
		private set => _health = value;
	}

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
	
	public void DieFromBombExplosion(bool getThrownBack)
	{
		if(_my.isDead) return;
		
		if(_my.TryGetComponent(out EnemySheildController enemySheildController))
			if (!enemySheildController.IsSheildBroken) 
				return;
		

		_my.isDead = true;
		if (!isEnemySideWalk)
		{
			_my.Movement.StopMovement();
			_my.Movement.DisableAgent();
		}
		
		ResetHealthAfterDie();
			
		_my.RagdollController.GoRagdoll(getThrownBack);
		
		disableAllBombsOnMe();
		
		EnemyEvents.InvokeOnEnemyDied(this);
		
		AudioManager.instance.Play("Die");
	}
	private void StartChasingPlayer()
	{
		_my.Movement.SetChaseTarget(_player);
		_my.Movement.ChasePlayer(true);
	}
	private void OnReachNextArea()
	{
		print("onreach next area");
		
		print("Enemy area: " + _my.gameObject.name + _my.area);
		print("level flow area: " + _my.gameObject.name + LevelFlowController.only.currentArea);
		
		
		if(_my.area != LevelFlowController.only.currentArea) return;
		print("Reach next area enemy");

		if (isEnemyHostageHold) return;

		if (isEnemySideWalk) return;
		
		DOVirtual.DelayedCall(Random.Range(0, 0.5f), () =>
		{
			StartChasingPlayer();
		});

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
		
		if(_my.TryGetComponent(out EnemySheildController enemySheildController))
			if (!enemySheildController.IsSheildBroken) 
				return false;
		
		
		if(isEnemySideWalk)
			if (ShouldEnemySideWalkOneShotRagdoll)
			{
				ResetHealthAfterDie();
				_my.RagdollController.GoRagdoll(true);
				EnemyEvents.InvokeOnEnemyDied(this);
				return true;
			}
		
		
		_health -= damage;
		healthCanvas.SetHealth(_health);
		_my.Animations.GetHit();
		AudioManager.instance.Play("ArrowHit");
		
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

		if (!isEnemySideWalk)
		{
			_my.Movement.StopMovement();
			_my.Movement.DisableAgent();
		}
		
		ResetHealthAfterDie();
			
		_my.RagdollController.GoRagdoll(getThrownBack);
		disableAllBombsOnMe();
			
		EnemyEvents.InvokeOnEnemyDied(this);
		
		AudioManager.instance.Play("Die");
		
	}

	public void ResetHealthAfterDie()
	{
		_health = 0f;
		healthCanvas.SetHealth(1f);
		healthCanvas.DisableCanvas();
	}


	public bool OnStick(GameObject bomb,Transform target)
	{
		
		if (stickingBehaviour != IStickable.StickableBehaviour.Stickable) return false;
		
		if(_my.TryGetComponent(out EnemySheildController enemySheildController))
			if (!enemySheildController.IsSheildBroken) 
				return false;
		
		var bombController = bomb.GetComponent<BombController>();
		if(bombController==null) return false;
		bombController.myParent = gameObject;
		bombController.IAmOnEnemy = true;
		AddBomb(bomb);
		bomb.transform.parent = target;
		_my.Animations.GetHit();
		
		AudioManager.instance.Play("BombHit");
		
		return true;
	}

	public bool OnExplodeDamage(GameObject bomb)
	{
		if (explodBehaviour != IExplodDamageable.ExplodableBehaviour.Explodable) return false;
		
		if(_my.TryGetComponent(out EnemySheildController enemySheildController))
			if (!enemySheildController.IsSheildBroken) 
				return false;
		
		DOVirtual.DelayedCall(0.15f, ()=>DieFromBombExplosion(true));
		return true;

	}
}
