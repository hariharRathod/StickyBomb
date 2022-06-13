using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	private NavMeshAgent _agent;
	private Transform _currentTarget;

	private bool _chasing, _chasingPlayer;

	private Transform _transform;
	
	private EnemyRefbank _my;
	[SerializeField] private float playerAttackDistance;
	[SerializeField] private float enemyAttackDistance;

	private void Start()
	{
		_my = GetComponent<EnemyRefbank>();
		_agent = GetComponent<NavMeshAgent>();
		_transform = transform;
		
	}
	
	private void Update()
	{
		if(!_chasing) return;
		if(Vector3.Distance(_currentTarget.position, _transform.position) >
		   (_chasingPlayer ? playerAttackDistance : enemyAttackDistance)) return;
			
		StopMovement();
		DoAttack();
	}
	
	private void DoAttack()
	{
		/*var dir = _currentTarget.position - transform.position;
		transform.DORotateQuaternion(Quaternion.LookRotation(dir), 0.5f);*/
		if (_chasingPlayer) _my.Animations.DoAttack();
		
	}
	
	public void SetChaseTarget(Transform target)
	{
		if(_my.isDead) return;
		
			
		_agent.enabled = true;
		_agent.isStopped = false;
		_agent.SetDestination(target.position);
			
		_my.Animations.StartWalking();
		_currentTarget = target;
		_chasing = true;
	}

	public void ChasePlayer(bool status) => _chasingPlayer = status;
	public void DisableAgent() => _agent.enabled = false;
	
	public void StopMovement()
	{
		if(!_agent.enabled) return;
			
		_agent.isStopped = true;
		_my.Animations.StopWalking();
		_chasing = false;
	}
}
