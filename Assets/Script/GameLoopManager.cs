
using DG.Tweening;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{

	[SerializeField] private float slowMotionTimeScale;
	[SerializeField] private bool shouldCameraFollow;
	private float _startTimeScale, _startFixedDeltaTime;

	private static bool _inSlowMotion;


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
		print("Inside checkfor camera follow");
		if (!shouldCameraFollow) return;
		print("shouldfollow ke agge");
		if (target.CompareTag("Ground")) return;
		print("ground cehck ke aage");
		if (!LevelFlowController.only.IsThisLastEnemy()) return;
		print("is this last enemy");
		if (target.CompareTag("Bomb"))
		{
			OnFollowArrowIsTrue();
			print("bomb");
			return;
		}

		if (target.CompareTag("TargetEnemy"))
		{
			print("target enemy");
			if (target.transform.root.TryGetComponent(out EnemyRefbank enemyRefbank) &&
				target.transform.TryGetComponent(out EnemyBodyCollider enemyBodyCollider))
			{
				
				var currentHealth = enemyRefbank.Controller.Health - enemyBodyCollider.Damage;
				if (currentHealth > 0) return;
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



}
