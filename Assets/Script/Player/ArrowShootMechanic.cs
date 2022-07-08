using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowShootMechanic : MonoBehaviour
{
	[SerializeField] private GameObject arrowPrefab,arrow,bowHolder;
	[SerializeField] private Transform startPoint,hitMarker;
	[SerializeField] private float aimSpeedHorizontal;
	[SerializeField] private float aimSpeedVertical;
	[SerializeField] private float clampAngleHorizontal;
	[SerializeField] private float clampAngleVertical;
	[SerializeField] private float multipleArrowRadius;

	private float _rotX, _rotY, _initRotAxisX, _initRotAxisY;
	
	private PlayerRefBank _my;
	private bool _shooted;
	private Vector3 _targetPos;
	private Transform _player,_targetTransform;
	private Quaternion _areaInitRotation;
	private Tween _arrowRotation;

	private int _arrowsCount = 0;
	
	[SerializeField] private int shootSpeedMin,shootSpeedMax;
	private int _shootSpeed;

	private List<GameObject> _arrowsFromIncrementGateList;
	[SerializeField] private float arrowSpeed;
	[SerializeField] private float arrowSpeedAfterDotween;

	public int ArrowsCount
	{
		get => _arrowsCount;
		set => _arrowsCount = value;
	}

	public Transform HitMarker
	{
		get => hitMarker;
		
	}

	private void OnEnable()
	{
		WeaponEvents.OnArrowSelectEvent += OnArrowWeaponSelected;
		WeaponEvents.OnBombSelectEvent += OnBombWeaponSelected;
		GameEvents.GameWin += OnGameWin;
		GameEvents.CameraFollowArrowStart += OnCameraFollowArrowStart;
		
	}

	private void OnDisable()
	{
		WeaponEvents.OnArrowSelectEvent -= OnArrowWeaponSelected;
		WeaponEvents.OnBombSelectEvent -= OnBombWeaponSelected;
		GameEvents.GameWin -= OnGameWin;
		GameEvents.CameraFollowArrowStart -= OnCameraFollowArrowStart;
		
	}

	
	private void Start()
	{
		_my = GetComponent<PlayerRefBank>();
		_player = GameObject.FindGameObjectWithTag("PlayerHolder").transform;
		
		_arrowsFromIncrementGateList=new List<GameObject>();
		
		/*_areaInitRotation = _player.rotation;
		var rot = _areaInitRotation.eulerAngles;

		_initRotAxisX = rot.x;
		_initRotAxisY = rot.y;
		
		_rotY = rot.y;
		_rotX = rot.x;*/
	}
	
	private void OnArrowWeaponSelected()
	{
		bowHolder.SetActive(true);
		//hitMarker.gameObject.SetActive(true);
	}
	
	private void OnBombWeaponSelected()
	{
		bowHolder.SetActive(false);
		hitMarker.gameObject.SetActive(false);
	}

	public void ArrowAim(RaycastHit hitInfo,Vector3 hitPoint)
	{
		//hitMarker.gameObject.SetActive(true);
		_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowAim,true);
		_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowShoot,false);
		
		//enable if hit marker is allowed to enable.
		/*var position = _my.Camera.transform.position;
		Debug.DrawLine(position, hitPoint, Color.red, 5f, false);
		
		if(hitInfo.collider.CompareTag("Ground"))
			hitMarker.position = hitInfo.point + hitInfo.normal * 0.01f;
		
		if(hitInfo.collider.CompareTag("TargetEnemy"))
			hitMarker.position = hitInfo.point + hitInfo.normal * 0.1f;
		
		if(hitInfo.collider.CompareTag("ExplosiveBarrel"))
			hitMarker.position = hitInfo.point + hitInfo.normal * 0.08f;
		
		if(hitInfo.collider.CompareTag("ShieldSurface"))
			hitMarker.position = hitInfo.point + hitInfo.normal * 0.08f;
		
		if(hitInfo.collider.CompareTag("IncrementGate"))
			hitMarker.position = hitInfo.point + hitInfo.normal * 0.05f;
		
		hitMarker.rotation = Quaternion.LookRotation(hitInfo.normal);*/
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

	public void ShootAnyWhere(Vector3 hitPoint)
	{
		InputHandler.PutInCoolDown();
		_targetPos = hitPoint;
		_targetTransform = null;
		_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowShoot,true);
		
		DOVirtual.DelayedCall(0.3f, () =>
		{
			if (GameLoopManager.InSlowMotion) return;
			arrow.SetActive(true);
		}).OnComplete(
			() =>
			{
				//_player.rotation = _playerDefaultRotation;
			});
	}
	

	public void Shoot(Transform hitTransform,Vector3 hitPoint)
	{
		
		InputHandler.PutInCoolDown();
		_targetPos = hitPoint;
		_targetTransform = hitTransform;
		_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowShoot,true);
		
	}

	public void DisableArrowShoot()
	{
		_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowShoot,false);
	}


	public void OnShootAnimation()
	{
		arrow.SetActive(false);
		//ye yaha karra hu taki slow motion me arrow activate hota na dikhe camera me
		DOVirtual.DelayedCall(0.3f, () =>
		{
			if (GameLoopManager.InSlowMotion) return;
			arrow.SetActive(true);
		}).OnComplete(
			() =>
			{
				//_player.rotation = _playerDefaultRotation;
			});
		LaunchArrow();
		Vibration.Vibrate(10);
	}

	public void LaunchArrow()
	{
		var arrow = Instantiate(arrowPrefab, startPoint.position, startPoint.rotation);
		var rb = arrow.GetComponent<Rigidbody>();
		var dirToTarget = _targetPos - arrow.transform.position;
		arrow.transform.rotation = Quaternion.LookRotation(dirToTarget,Vector3.up);
		if(_targetTransform)
			WeaponEvents.InvokeOnArrowRealeaseEvent(_targetTransform,arrow);
		
		//ye ek quick fix hai to the problem of arrrow colliding and dropping down.
		//rb.AddForce(arrowSpeed * dirToTarget,ForceMode.Impulse);
		
		
		//ye dot
		//dotween se move karra hai arrow ko,isme kuch problem hai shyad isliye ye abhi ke liye comment kiya hai
		arrow.transform.DOMove(_targetPos, 0.2f).SetUpdate(UpdateType.Fixed).SetEase(Ease.Linear).OnComplete(
			() =>
			{
				rb.AddForce(arrowSpeedAfterDotween * dirToTarget, ForceMode.Impulse);
				//arrow.transform.DORotateQuaternion(Quaternion.LookRotation(dirToTarget, Vector3.up), 0.1f);
				HideHitMarker();
				//arrow.transform.DOMoveZ(arrow.transform.position.z + 0.05f, 0.01f);
				//rb.isKinematic = true;
			});
	
	}
	
	private void OnGameWin()
	{
		HideHitMarker();
	}
	
	private void OnCameraFollowArrowStart()
	{
		HideHitMarker();
	}

	public void HideHitMarker()
	{
		hitMarker.gameObject.SetActive(false);
	}

	public void ShootMutipleArrows(int number, GateType gateType, Transform initialArrow, Transform spwanPoint)
	{
		switch (gateType)
		{
			case GateType.Add:
			{
				_arrowsCount = number;
			}
				break;
			case GateType.Multiply:
			{
				_arrowsCount = number;
			}
				break;
		}

		if (_arrowsCount <= 0) return;

		if (_arrowsFromIncrementGateList.Contains(initialArrow.gameObject)) return;
		
		initialArrow.gameObject.SetActive(false);

		for (int i = 0; i < _arrowsCount; i++)
		{
			var radians = (Mathf.PI) / _arrowsCount * i;

			var vertical = Mathf.Sin(radians);
			var horizontal = Mathf.Cos(radians);
			
			var spwanDir= new Vector3(horizontal,0,vertical);

			var position = initialArrow.position;
			var spwanpos = position + spwanDir * multipleArrowRadius;
			var random = Random.Range(-1f, 2f);
			random *= 0.4f;
			spwanpos.y = position.y + random;
			

			var arrow = Instantiate(arrowPrefab, spwanpos, quaternion.identity) as GameObject;
			//arrow.GetComponent<MeshCollider>().enabled = false;
			var to = arrow.transform.rotation;
			var from = Quaternion.LookRotation(spwanpos - position);
			var rot = Quaternion.RotateTowards(to,from,10f);
			arrow.transform.rotation = rot;

			_arrowsFromIncrementGateList.Add(arrow);


			if (!arrow.TryGetComponent(out ArrowShootProjectileController arrowShootProjectileController))
			{
				arrow.SetActive(false);
				continue;
			}
			
			arrowShootProjectileController.ShootArrowInProjectile(shootSpeedMin,shootSpeedMax);

		}
		
		DOVirtual.DelayedCall(20f, () =>
		{
			if (_arrowsFromIncrementGateList.Count < 15) return;

			for (int i = 0; i < 10; i++)
			{
				_arrowsFromIncrementGateList[i].SetActive(false);
			}
		
		});

	}
	
}
