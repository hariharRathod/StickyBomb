
using DG.Tweening;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{

	[SerializeField] private float slowMotionTimeScale;
	[SerializeField] private bool shouldCameraFollow;
	private float _startTimeScale, _startFixedDeltaTime;

	private static bool _inSlowMotion;

	private bool _follow,_enterCheckForCameraFollow;

	private Transform _target;

	public static bool InSlowMotion => _inSlowMotion;
	
	private void OnEnable()
	{
		WeaponEvents.ArrowRealeaseEvent += CheckForCameraFollow;
		WeaponEvents.ArrowCollisonWithTargetDone += OnArrowCollisonWithTarget;
	}



	private void OnDisable()
	{
		WeaponEvents.ArrowRealeaseEvent -= CheckForCameraFollow;
		WeaponEvents.ArrowCollisonWithTargetDone -= OnArrowCollisonWithTarget;
	}

	

	private void Start()
	{
		_startTimeScale = Time.timeScale;
		_startFixedDeltaTime = Time.fixedDeltaTime;
		
	}

	private void Update()
	{
		//ye yaha kyu hai,thikk karo isse......................
		if (!_enterCheckForCameraFollow) return;
		_enterCheckForCameraFollow = false;
		if (_follow) return;
		if (!_target) return;
		if (!_target.transform.root.TryGetComponent(out EnemyRefbank enemyRefBank)) return;
		enemyRefBank.Animations.SetAnimatorStatus(true);
	
	}


	public void StartSlowMotion()
	{
		print("In slowmotion");
		Time.timeScale = slowMotionTimeScale;
		Time.fixedDeltaTime = _startFixedDeltaTime * slowMotionTimeScale;
	}

	public void StopSlowMotion()
	{
		Time.timeScale = _startTimeScale;
		Time.fixedDeltaTime = _startFixedDeltaTime;
	}

	private void CheckForCameraFollow(Transform target, GameObject arrow)
	{
		_target = target;
		_enterCheckForCameraFollow = true;
		SwitchOffAnimatorOfTarget(target);
		//print("Inside checkfor camera follow");
		if (!shouldCameraFollow){ _follow=false; return;}
		//print("shouldfollow ke agge");
		if (target.CompareTag("Ground"))
		{
			_follow = false;  return;}
		//print("ground cehck ke aage");
		
		
		if (!LevelFlowController.only.IsThisLastEnemy())
		{
			_follow = false; return;}
		//print("is this last enemy");
		if (target.CompareTag("Bomb"))
		{
			print("bomb");
			Debug.DrawLine(Camera.main.transform.position, target.position, Color.yellow, 5f, false);
			_follow = true;
			OnFollowArrowIsTrue();
			
			
			return;
		}

		if (target.CompareTag("TargetEnemy"))
		{
			print("target enemy");
			if (target.transform.root.TryGetComponent(out EnemyRefbank enemyRefbank) &&
				target.transform.TryGetComponent(out EnemyBodyCollider enemyBodyCollider))
			{
				print("found components on target");
				Debug.DrawLine(Camera.main.transform.position, target.position, Color.yellow, 5f, false);
				var currentHealth = enemyRefbank.Controller.Health - enemyBodyCollider.Damage;
				if (currentHealth > 0) return;
				_follow = true;
				OnFollowArrowIsTrue();
				print("current health check");
				
			}
		}
	}


	private void OnFollowArrowIsTrue()
	{
		GameEvents.InvokeOnCameraFollowArrowStart();
		StartSlowMotion();
		_inSlowMotion = true;
		print("yeahhhh Start slow motion");

	}
	
	private void OnArrowCollisonWithTarget()
	{
		if (!_inSlowMotion) return;
		
		DOVirtual.DelayedCall(0.5f,StopSlowMotion);
	}


	public static void SwitchOffAnimatorOfTarget(Transform target)
	{
		if (!target) return;

		if (!target.root.TryGetComponent(out EnemyRefbank enemyRefBank)) return;
		
		enemyRefBank.Animations.SetAnimatorStatus(false);
		print("setting animator in game loop manager false");

	}


}
