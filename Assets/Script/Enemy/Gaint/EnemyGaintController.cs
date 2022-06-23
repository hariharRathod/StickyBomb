using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyGaintController : MonoBehaviour
{
	private EnemyRefbank _my;

	[SerializeField] private float onBombExplosionDamage,onArrowCollisonDamage;
	[SerializeField] private GameObject gaintPropToThrow;
	[SerializeField] private Transform propHolder;

	private GameObject _currentBarrelToThrow;
	private bool _isAttacking, _canAttack;
	private Transform _gaintPos,_playerPos;

	private void OnEnable()
	{
		GameEvents.ReactNextArea += OnReachNextArea;
	}

	private void OnDisable()
	{
		GameEvents.ReactNextArea -= OnReachNextArea;
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

	public void OnStick(GameObject bomb,Transform target)
	{
		
	}



	public void OnExplodDamage()
	{
		
	}


	public void HitDamageFromArrow()
	{
		_my.Controller.GetHit(onArrowCollisonDamage);
	}

	public void HitDamageFromBomb()
	{
		_my.Controller.GetHit(onBombExplosionDamage);
	}

	private void StartGaintBehaviour()
	{
		_gaintPos = transform;
		_playerPos = GameObject.FindGameObjectWithTag("PlayerRoot").transform;
		_canAttack = true;
		AttackCycle();

	}
	
	private void OnReachNextArea()
	{
		StartGaintBehaviour();
	}

}
