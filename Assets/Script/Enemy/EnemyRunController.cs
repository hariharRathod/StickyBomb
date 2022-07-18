using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyRunController : MonoBehaviour
{
	private EnemyRefbank _my;
	private Animator _anim;
	[SerializeField] private float fastMoveSpeedMultiplier = 1.2f, maxDistanceToPlayer,hostageMaxDistance;
	[SerializeField] private Transform hostageTakenPoint;
	[SerializeField] private ParticleSystem dustOnRun;
	private HostageRefBank _hostageRefBank;
	
	private Transform _player, _transform;
	private float _currentSpeedMultiplier = 1f, _myMaxDistance;
	
	private bool _hasTappedToPlay,_stopRunning;
	private readonly List<Transform> _colliders = new List<Transform>();
	
	private static readonly int Run = Animator.StringToHash("run");
	private static readonly int AttackGirl= Animator.StringToHash("attackGirl");
	private static readonly int BackToIdle= Animator.StringToHash("backtoidle");
	private Transform _transformRoot;
	private bool _slowedDownTemporarily,_isDead;

	public bool IsDead
	{
		get => _isDead;
	}

	private void Start()
	{
		_my = GetComponent<EnemyRefbank>();
		_anim = GetComponent<Animator>();
		_hostageRefBank = GameObject.FindWithTag("HostageRoot").GetComponent<HostageRefBank>();
		_stopRunning = false;
		_transform = transform;
		_transformRoot = _transform.root;
		_player = GameObject.FindGameObjectWithTag("PlayerRoot").transform;
		_currentSpeedMultiplier = fastMoveSpeedMultiplier;
		_myMaxDistance = Random.Range(maxDistanceToPlayer - 4f, maxDistanceToPlayer);
	}
	
	/*private void OnEnable()
	{
		GameEvents.TapToPlay += OnTapToPlay;
		GameEvents.GameLose += OnGameLose;
		GameEvents.GameWin += OnGameWin;
	}

	private void OnDisable()
	{
		GameEvents.TapToPlay -= OnTapToPlay;
		GameEvents.GameLose -= OnGameLose;
		GameEvents.GameWin -= OnGameWin;
	}*/
	private void Update()
	{
		if (!_hasTappedToPlay) return;
		if (_my.isDead) return;
		if (_stopRunning) return;
		
		_transform.position += Vector3.forward * (VehicleMovement.MovementSpeed * _currentSpeedMultiplier * Time.deltaTime);
		
		if(_slowedDownTemporarily) return;
		if(Vector3.Distance(_transform.position, _player.position) > _myMaxDistance) return;
		_currentSpeedMultiplier = 1f;
	
	}
	
	private bool IsSomeoneInFront() => _colliders.Count > 0;
	private void OnTriggerEnter(Collider other)
	{
		if(!other.CompareTag("TargetEnemy")) return;
		if(other.transform.root == _transformRoot) return;
		
		if (_colliders.Contains(other.transform.root)) return;
		
		_colliders.Add(other.transform.root);
		
		_currentSpeedMultiplier = 0.5f;
		_slowedDownTemporarily = true;
	}

	private void OnTriggerExit(Collider other)
	{
		if(!other.CompareTag("TargetEnemy")) return;
		if(other.transform.root == _transformRoot) return;
		if(!IsSomeoneInFront()) return;
		if (!_colliders.Contains(other.transform.root)) return;
		
		_colliders.Remove(other.transform.root);
		
		if(IsSomeoneInFront()) return;
		
		_currentSpeedMultiplier = fastMoveSpeedMultiplier;
		_slowedDownTemporarily = false;
	}

	public void GetHit(float damage,bool shouldHit)
	{
		DisableAnimator();
		_my.RagdollController.GoRagdollWhileRunning(true);
		_isDead = true;
		EnemyEvents.InvokeOnEnemyDied(_my.Controller);
		AudioManager.instance.Play("Die");
	}

	public void DisableAnimator()
	{
		_anim.enabled = false;
	}

	private void RunAnim()
	{
		_anim.SetTrigger(Run);
	}

	private void AttackGirlAnim()
	{
		_anim.SetTrigger(AttackGirl);
	}

	/*private void HitGirl()
	{
		if (_hostageRefBank.Controller.IsDead){_anim.SetBool(BackToIdle,true); return;}
		LevelFlowController.only.TryAssignMinDistance(Vector3.Distance(_transform.position,_hostageRefBank.transform.position), this);
		
		DOVirtual.DelayedCall(0.15f, () =>
		{
			if (LevelFlowController.only.closest == this)
			{
				print("closet enemy: " + LevelFlowController.only.closest.gameObject.name);
				var position = _hostageRefBank.RunController.EnemySpot.position;
				var dir = position - _transform.position;
				_transform.DOMove(position, 0.9f).OnStart(() =>
				{
					print("moving towards hostage girl");
					Quaternion rotation=Quaternion.LookRotation(dir,Vector3.up);
					_transform.rotation = rotation;
				}).OnComplete(()=>{
					
					DOVirtual.DelayedCall(0.1f, () =>
					{
						 AttackGirlAnim();
						_hostageRefBank.RunController.HostageTakenAnim();
						_hostageRefBank.RunController.PositionHostageTaken(hostageTakenPoint);
						//_hostageRefBank.Controller.HostageDie(false);
						Quaternion rotation=Quaternion.Euler(0,180,0);
						transform.rotation = rotation;
					});
					
				
				});
			}
			else
			{
				_anim.SetBool(BackToIdle,true);
			}

		});
		
	}*/

	public void DustOnRunningAnimation()
	{
		dustOnRun.Play();
	}

	private void OnTapToPlay()
	{
		RunAnim();
		_hasTappedToPlay = true;
		_transform.rotation = Quaternion.Euler(0,180,0);
	}
	
	/*private void OnGameLose()
	{
		_stopRunning = true;
		if(_hostageRefBank)
			HitGirl();
	}*/
	
	private void OnGameWin()
	{
		_stopRunning = true;
		
	}
}
