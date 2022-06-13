using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator _anim;
	
	private static readonly int IsWalking = Animator.StringToHash("isWalking");
	private static readonly int Hit = Animator.StringToHash("hit");
	private static readonly int Attack = Animator.StringToHash("attack");
	
	private EnemyRefbank _my;

	
	private void Start()
	{
		_anim=GetComponent<Animator>();
		_my = GetComponent<EnemyRefbank>();
	}
	
	
	

	public void StartWalking()
	{
		_anim.SetBool(IsWalking,true);
	}

	public void StopWalking()
	{
		_anim.SetBool(IsWalking,false);
	}


	public void GetHit()
	{
		_anim.SetTrigger(Hit);
	}
	
	
	public void DoAttack()
	{
		if (_my.isDead) return;
			
		_anim.SetTrigger(Attack);
		GameEvents.InvokeOnGameLose();
	}
	public void SetAnimatorStatus(bool status) => _anim.enabled = status;
}
