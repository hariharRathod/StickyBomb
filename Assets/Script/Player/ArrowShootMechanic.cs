using DG.Tweening;
using UnityEngine;

public class ArrowShootMechanic : MonoBehaviour
{
	[SerializeField] private GameObject arrowPrefab,arrow,bowHolder;
	[SerializeField] private Transform startPoint,hitMarker;
	[SerializeField] private float aimSpeedHorizontal;
	[SerializeField] private float aimSpeedVertical;
	[SerializeField] private float clampAngleHorizontal;
	[SerializeField] private float clampAngleVertical;
	
	
	private float _rotX, _rotY, _initRotAxisX, _initRotAxisY;
	
	private PlayerRefBank _my;
	private bool _shooted;
	private Vector3 _targetPos;
	private Transform _player;
	private Quaternion _playerDefaultRotation;

	public Transform HitMarker
	{
		get => hitMarker;
		
	}

	private void OnEnable()
	{
		WeaponEvents.OnArrowSelectEvent += OnArrowWeaponSelected;
		WeaponEvents.OnBombSelectEvent += OnBombWeaponSelected;
		GameEvents.GameWin += OnGameWin;
	}

	private void OnDisable()
	{
		WeaponEvents.OnArrowSelectEvent -= OnArrowWeaponSelected;
		WeaponEvents.OnBombSelectEvent -= OnBombWeaponSelected;
		GameEvents.GameWin -= OnGameWin;
	}


	private void Start()
	{
		_my = GetComponent<PlayerRefBank>();
		_player = GameObject.FindGameObjectWithTag("PlayerRoot").transform;
		_playerDefaultRotation = _player.rotation;
	}
	
	private void OnArrowWeaponSelected()
	{
		bowHolder.SetActive(true);
		hitMarker.gameObject.SetActive(true);
	}
	
	private void OnBombWeaponSelected()
	{
		bowHolder.SetActive(false);
		hitMarker.gameObject.SetActive(false);
	}

	public void ArrowAim(RaycastHit hitInfo,Vector3 hitPoint)
	{

		_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowAim,true);
		_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowShoot,false);

		print(hitPoint);
		var position = _player.position;
		Debug.DrawLine(position, hitPoint, Color.red, 5f, false);
		
	
		
		hitMarker.position = hitInfo.point + hitInfo.normal * 0.12f;
		hitMarker.rotation = Quaternion.LookRotation(hitInfo.normal);
		
		
	}
	
	public void Aim(Vector2 inputDelta)
	{
		_rotY += inputDelta.x * aimSpeedHorizontal * Time.deltaTime;
		_rotX -= inputDelta.y * aimSpeedVertical * Time.deltaTime;
 
		_rotY = Mathf.Clamp(_rotY, _initRotAxisY - clampAngleHorizontal, _initRotAxisY + clampAngleHorizontal);
		_rotX = Mathf.Clamp(_rotX, _initRotAxisX - clampAngleVertical, _initRotAxisX + clampAngleVertical);

		var newRot = Quaternion.Euler(_rotX, _rotY, 0.0f);
		_player.rotation = newRot;
	}

	public void Shoot(Vector3 hitPoint)
	{
		_targetPos = hitPoint;
		_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowShoot,true);
		DOVirtual.DelayedCall(0.5f, () => arrow.SetActive(true)).OnComplete(
			() =>
			{
				//_player.rotation = _playerDefaultRotation;
			});

	}


	public void OnShootAnimation()
	{
		arrow.SetActive(false);
		LaunchArrow();
	}

	public void LaunchArrow()
	{
		var arrow = Instantiate(arrowPrefab, startPoint.position, startPoint.rotation);

		var rb = arrow.GetComponent<Rigidbody>();
		var dirToTarget = _targetPos - arrow.transform.position;
		//arrow.transform.rotation = Quaternion.LookRotation(dirToTarget,Vector3.up);

		arrow.transform.DORotateQuaternion(Quaternion.LookRotation(dirToTarget, Vector3.up), 0.1f);
		arrow.transform.DOMove(_targetPos, 0.25f).SetEase(Ease.Linear).OnComplete(
			() =>
			{
				
				//rb.isKinematic = true;
			});
		

	}
	
	private void OnGameWin()
	{
		hitMarker.gameObject.SetActive(false);
	}
}
