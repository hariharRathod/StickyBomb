using DG.Tweening;
using UnityEngine;

public class EnemyRunController : MonoBehaviour
{
	private EnemyRefbank _my;
	private Animator _anim;
	[SerializeField] private float fastMoveSpeedMultiplier = 1.2f, maxDistanceToPlayer;
	[SerializeField] private HostageController hostageController;
	
	private Transform _player, _transform;
	private float _currentSpeedMultiplier = 1f, _myMaxDistance;
	
	private bool _hasTappedToPlay,_stopRunning;
	
	private static readonly int Run = Animator.StringToHash("run");
	private static readonly int AttackGirl= Animator.StringToHash("attackGirl");

	private void Start()
	{
		_my = GetComponent<EnemyRefbank>();
		_anim = GetComponent<Animator>();
		_stopRunning = false;
		_transform = transform;
		_player = GameObject.FindGameObjectWithTag("PlayerRoot").transform;
		_currentSpeedMultiplier = fastMoveSpeedMultiplier;
		_myMaxDistance = Random.Range(maxDistanceToPlayer - 4f, maxDistanceToPlayer);
	}
	
	private void OnEnable()
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
	}
	private void Update()
	{
		if (!_hasTappedToPlay) return;
		if (_my.isDead) return;
		if (_stopRunning) return;
		
		_transform.position += Vector3.forward * (VehicleMovement.MovementSpeed * _currentSpeedMultiplier * Time.deltaTime);
		
		if(Vector3.Distance(_transform.position, _player.position) > _myMaxDistance) return;
		_currentSpeedMultiplier = 1f;
	
	}

	public void GetHit(float damage,bool shouldHit)
	{
		_anim.enabled = false;
		_my.RagdollController.GoRagdollWhileRunning(true);
		EnemyEvents.InvokeOnEnemyDied(_my.Controller);
		AudioManager.instance.Play("Die");
	}

	private void RunAnim()
	{
		_anim.SetTrigger(Run);
	}

	private void AttackGirlAnim()
	{
		_anim.SetTrigger(AttackGirl);
	}

	private void HitGirl()
	{
		if (hostageController.IsDead) return;
		
		var dir = hostageController.transform.position - transform.position;
		transform.DORotateQuaternion(Quaternion.LookRotation(dir), 0.3f).OnComplete(() =>
		{
			transform.DOMove(hostageController.transform.position, 0.2f);
			AttackGirlAnim();
			hostageController.HostageDie(false);
		});
		
	}

	private void OnTapToPlay()
	{
		RunAnim();
		_hasTappedToPlay = true;
		_transform.rotation = Quaternion.Euler(0,180,0);
	}
	
	private void OnGameLose()
	{
		_stopRunning = true;
		HitGirl();
	}
	
	private void OnGameWin()
	{
		_stopRunning = true;
		
	}
}
