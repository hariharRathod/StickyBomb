using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyGaintController : MonoBehaviour,IStickable,IExplodDamageable
{
	private EnemyGaintRefBank _my;
	private Transform _player;

	[SerializeField] private float onBombExplosionDamage,onArrowCollisonDamage;
	[SerializeField] private GameObject gaintPropToThrow;
	[SerializeField] private Transform propHolder;

	[SerializeField] private bool shouldAttackWithBarrel;
	
	[SerializeField] private IStickable.StickableBehaviour stickingBehaviour;
	[SerializeField] private IExplodDamageable.ExplodableBehaviour explodBehaviour;
	
	[SerializeField] private HealthCanvas healthCanvas;

	[SerializeField] private List<GameObject> headGear;


	public List<GameObject> HeadGear => headGear;


	private float _health=1f;

	private GameObject _currentBarrelToThrow;
	private bool _isAttacking, _canAttack;
	private Transform _gaintPos,_playerPos;
	private List<GameObject> _bombsList;

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
		_my = GetComponent<EnemyGaintRefBank>();
		_bombsList=new List<GameObject>();
		_player = GameObject.FindGameObjectWithTag("PlayerRoot").transform;
		
	}


	private void GeneratePropToThrow()
	{
		_currentBarrelToThrow = Instantiate(gaintPropToThrow,propHolder.position,Quaternion.identity);
		_currentBarrelToThrow.transform.localScale = Vector3.zero;
		_currentBarrelToThrow.transform.DOScale(Vector3.one, 0.45f).SetEase(Ease.Linear);

	}


	private void AttackCycle()
	{
		if (!_canAttack) return;

		DOVirtual.DelayedCall(3f, () =>
		{
			GeneratePropToThrow();
			
		});

	}

	public void disableAllBombsOnMe()
	{
		foreach (var bomb in _bombsList)
		{
			bomb.SetActive(false);
		}
	}
	
	private void StartGaintBehaviour()
	{
		_gaintPos = transform;
		_playerPos = GameObject.FindGameObjectWithTag("PlayerRoot").transform;

		if (!shouldAttackWithBarrel)
		{
			return;
			
		}
		_canAttack = true;
		AttackCycle();

	}
	
	public bool GetHit(float damage)
	{
		if (_my.isDead) return false;
		
		if(_my.TryGetComponent(out EnemySheildController enemySheildController))
			if (!enemySheildController.IsSheildBroken) 
				return false;
		
		
		_health -= damage;
		healthCanvas.SetHealth(_health);
		_my.GaintAnimations.GetHit();
		AudioManager.instance.Play("ArrowHit");
		
		if (_health > 0f)
		{
			return true;
		}

		DieFromHealth(true);
		_my.isDead = true;
		return false;

	}
	
	private void OnTapToPlay() => OnReachNextArea();
	
	private void DieFromHealth(bool getThrownBack)
	{
		if(_my.isDead) return;

		_my.isDead = true;
		_my.GaintMovement.StopMovement();
		_my.GaintMovement.DisableAgent();
		ResetHealthAfterDie();
			
		_my.GaintRagdollController.GoRagdoll(getThrownBack);
		disableAllBombsOnMe();
		
		EnemyEvents.InvokeOnGaintEnemyDied(this);
		
		AudioManager.instance.Play("Die");
		
	}
	
	public void ResetHealthAfterDie()
	{
		_health = 0f;
		healthCanvas.SetHealth(1f);
		healthCanvas.DisableCanvas();
	}
	
	private void StartChasingPlayer()
	{
		_my.GaintMovement.SetChaseTarget(_player);
		_my.GaintMovement.ChasePlayer(true);
	}
	
	public void AddBomb(GameObject bomb)
	{
		_bombsList.Add(bomb);
	}

	public void ArrowDamage()
	{
		GetHit(onArrowCollisonDamage);
	}

	public void BombDamage()
	{
		GetHit(onBombExplosionDamage);

		if (headGear.Count > 0)
		{
			if (headGear[0].TryGetComponent(out PropsController propsController))
			{
				propsController.ThrowBackProps();
				headGear.RemoveAt(0);
			}
		}
	}

	


	private void OnReachNextArea()
	{
		if(_my.area != LevelFlowController.only.currentArea) return;
		
		StartChasingPlayer();
	}

	public bool OnStick(GameObject bomb, Transform targetTransform)
	{
		if (stickingBehaviour != IStickable.StickableBehaviour.Stickable) return false;
		
		var bombController = bomb.GetComponent<BombController>();
		if(bombController==null) return false;
		bombController.myParent = gameObject;
		bombController.IAmOnEnemy = true;
		AddBomb(bomb);
		bomb.transform.parent = targetTransform;
		
		
		return true;
	}

	public bool OnExplodeDamage(GameObject bomb)
	{
		if (explodBehaviour != IExplodDamageable.ExplodableBehaviour.Explodable) return false;


		if (_my.isDead) return false;
		
		
		BombDamage();
		_my.GaintAnimations.GetHit();
		
		
		return true;
	}
}
