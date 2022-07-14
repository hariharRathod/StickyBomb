using DG.Tweening;
using UnityEngine;

public class HostageRunController : MonoBehaviour
{
	private Animator _anim;
	[SerializeField] private float fastMoveSpeedMultiplier = 1.2f, maxDistanceToPlayer;
	[SerializeField] private Transform enemySpot,hostageParentAfterLose;
	
	private Transform _player, _transform;
	private float _currentSpeedMultiplier = 1f, _myMaxDistance;
	
	
	private HostageController _hostageController;

	private bool _hasTappedToPlay,_stopRunning;
	
	private static readonly int Win = Animator.StringToHash("win");
	private static readonly int Run = Animator.StringToHash("run");
	private static readonly int Lost = Animator.StringToHash("lost");
	private static readonly int HostageTaken = Animator.StringToHash("hostagetaken");

	public Transform EnemySpot => enemySpot;

	private void Start()
	{
		_anim = GetComponent<Animator>();
		_stopRunning = false;
		_hostageController = GetComponent<HostageController>();
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
		if (_hostageController.IsDead) return;
		if (_hostageController.HasWon) return;
		if (_stopRunning) return;
		
		_transform.position += Vector3.forward * (VehicleMovement.MovementSpeed * _currentSpeedMultiplier * Time.deltaTime);
		
		if(Vector3.Distance(_transform.position, _player.position) > _myMaxDistance) return;
		_currentSpeedMultiplier = 1f;
	
	}

	private void HostageWinAnim()
	{
		_anim.SetBool(Win,true);
	}

	private void HostageRunAnim()
	{
		_anim.SetTrigger(Run);
	}

	private void HostageLostAnim()
	{
		_anim.SetBool(Lost,true);
	}

	public void HostageTakenAnim()
	{
		_anim.SetTrigger(HostageTaken);
	}

	public void PositionHostageTaken(Transform hostagetakenpoint)
	{
		_transform.parent = hostagetakenpoint;
		_transform.localPosition =Vector3.zero;
	}

	private void OnTapToPlay()
	{
		if (!_hostageController) return;
		
		HostageRunAnim();
		_hasTappedToPlay = true;
		_transform.rotation = Quaternion.Euler(0,180,0);
	
	}
	
	private void OnGameLose()
	{
		_stopRunning = true;
		HostageLostAnim();
	}
	
	private void OnGameWin()
	{
		_stopRunning = true;
		HostageWinAnim();
		
	}
	
}
