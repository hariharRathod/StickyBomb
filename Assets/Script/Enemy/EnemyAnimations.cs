
using UnityEngine;
using DG.Tweening;

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
		print("anim set true: ");
	}

	public void StopWalking()
	{
		_anim.SetBool(IsWalking,false);
	}


	public void GetHit()
	{
		_anim.SetTrigger(Hit);
		
		//DOVirtual.DelayedCall(0.15f, () => _anim.ResetTrigger(Hit));
	}
	
	
	public void DoAttack()
	{
		if (_my.isDead) return;
			
		_anim.SetTrigger(Attack);
		GameEvents.InvokeOnGameLose();
	}
	public void SetAnimatorStatus(bool status) => _anim.enabled = status;
}
