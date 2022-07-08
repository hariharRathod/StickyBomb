using DG.Tweening;
using UnityEngine;

public class HostageController : MonoBehaviour,IStickable,IExplodDamageable
{
	private Animator _anim;
	[SerializeField] private Rigidbody[] rigidbodies;
	[SerializeField] private float throwBackForce, upForce;
	[SerializeField] private int area;
	[SerializeField] private GameObject helpGameObject;
	
	private bool _isDead;
	
	[SerializeField] private IStickable.StickableBehaviour stickingBehaviour;
	[SerializeField] private IExplodDamageable.ExplodableBehaviour explodBehaviour;
	
	private static readonly int Win = Animator.StringToHash("win");
	

	public bool IsDead
	{
		get => _isDead;
		set => _isDead = value;
	}

	private void OnEnable()
	{
		GameEvents.CurrentAreaAllEnemyKilled += OnCurrentAreaAllEnemyKilled;
		

	}

	private void OnDisable()
	{
		GameEvents.CurrentAreaAllEnemyKilled -= OnCurrentAreaAllEnemyKilled;
		
	}

	private void OnCurrentAreaAllEnemyKilled()
	{
		if (area != LevelFlowController.only.currentArea) return;
		
		helpGameObject.SetActive(false);
		Victory();
	}

	private void Start()
	{
		_anim = GetComponent<Animator>();
		foreach (var rb in rigidbodies) rb.isKinematic = true;
	}

	private void Victory()
	{
		_anim.SetBool(Win,true);
	}

	public bool OnStick(GameObject bomb, Transform targetTransform)
	{
		if (stickingBehaviour != IStickable.StickableBehaviour.Stickable) return false;
		
		var bombController = bomb.GetComponent<BombController>();
		if(bombController==null) return false;
		bombController.myParent = gameObject;
		bomb.transform.parent = targetTransform;
		
		return true;
	}

	public bool OnExplodeDamage(GameObject bomb)
	{
		
		if (explodBehaviour != IExplodDamageable.ExplodableBehaviour.Explodable) return false;
		
		HostageDie();
		
		return true;
	}


	public void HostageDie()
	{
		_isDead = true;
		_anim.enabled = false;
		var direction = -transform.forward;
		foreach (var rb in rigidbodies)
		{
			rb.isKinematic = false;
			rb.AddForce(direction * ( throwBackForce ) + Vector3.up * upForce, ForceMode.Impulse);
		}
		
		GameEvents.InvokeOnGameLose();
	}

	public void DieCustom()
	{
		DOVirtual.DelayedCall(0.75f, () =>
		{
			HostageDie();
			helpGameObject.SetActive(false);
		});
		
	}
}
