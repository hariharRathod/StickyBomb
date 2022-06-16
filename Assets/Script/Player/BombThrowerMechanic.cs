using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrowerMechanic : MonoBehaviour
{
	[SerializeField] private GameObject bombPrefab,bombHolder;
	[SerializeField] private Transform startPoint;
	
	
	[SerializeField] private float trajectoryMaxHeight = 1f, bananaTorque;

	[SerializeField] private Transform hitMarker;
	[SerializeField] private int trajectoryResolution = 30;
	
	private Vector3[] _trajectoryPath;

	private PlayerRefBank _my;

	private LineRenderer _line;
	private Transform _hitTransform;
	private Vector3 _lastHitOffset;
	private const float Gravity = -9.81f;

	private List<Vector3> linePoints= new List<Vector3>();

	private void OnEnable()
	{
		WeaponEvents.OnArrowSelectEvent += OnArrowWeaponSelected;
		WeaponEvents.OnBombSelectEvent += OnBombWeaponSelected;
	}

	private void OnDisable()
	{
		WeaponEvents.OnArrowSelectEvent -= OnArrowWeaponSelected;
		WeaponEvents.OnBombSelectEvent -= OnBombWeaponSelected;
	}

	private void Start()
	{
		_my = GetComponent<PlayerRefBank>();
		_line = GetComponent<LineRenderer>();
			
		_trajectoryPath = new Vector3[trajectoryResolution];
		_line.positionCount = trajectoryResolution - 1;
	}
	
	private void OnArrowWeaponSelected()
	{
		bombHolder.SetActive(false);
		hitMarker.gameObject.SetActive(false);
		HideTrajectory();
	}
	
	private void OnBombWeaponSelected()
	{
		bombHolder.SetActive(true);
		hitMarker.gameObject.SetActive(true);
	}

	public void Shoot(Transform hitTransform, Vector3 hitPoint)
	{
		_hitTransform = hitTransform;
		_lastHitOffset = hitPoint - hitTransform.position;
		_my.PlayerAnimation.Anim.SetTrigger(PlayerAnimations.Aim);
		HideTrajectory();
	}
	
	public void ThrowOnAnimation()
	{
		var idealDest = _hitTransform.position + _lastHitOffset;
		LaunchBomb(idealDest);

	}
	
	private float CalculateInitialVelocity(Vector3 startPosition, Vector3 endPoint, out Vector3 initialVelocity)
	{
		//Jai Sebastian Maharaj ki
		var displacementY = endPoint.y - startPosition.y;
		var displacementXZ = new Vector3(endPoint.x - startPosition.x, 0, endPoint.z - startPosition.z);

		var trajectoryHeight = displacementY > trajectoryMaxHeight
			? displacementY + .2f
			: trajectoryMaxHeight;

		var time = Mathf.Sqrt(-2 * trajectoryHeight / Gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / Gravity);
		var velocityY = Vector3.up * Mathf.Sqrt(-2 * Gravity * trajectoryHeight);
		var velocityXZ = displacementXZ / time;

		initialVelocity = velocityXZ + velocityY * -Mathf.Sign(Gravity);

		return time;
	}
	
	private float LaunchBomb(Vector3 hitPoint)
	{
		var bomb = Instantiate(bombPrefab, startPoint.position, startPoint.rotation);
		var rb = bomb.GetComponent<Rigidbody>();
		var time = CalculateInitialVelocity(startPoint.position, hitPoint, out var initialVelocity);
		rb.AddForce(initialVelocity, ForceMode.VelocityChange);
		rb.AddTorque(Vector3.right * bananaTorque, ForceMode.VelocityChange);
		InputHandler.PutInCoolDown();
		return time;
	}
	
	
	public void HideTrajectory()
	{
		hitMarker.position = transform.position - transform.forward * 5f;
		_line.positionCount = 0;
		
	}
	
	public void DrawTrajectory(RaycastHit hitInfo)
	{
		var startPosition = startPoint.position;
		var time = CalculateInitialVelocity(startPosition, hitInfo.point, out var initialVelocity);
		var previousDrawPoint = startPosition;

		const int resolution = 30;
		for (var i = 1; i <= resolution; i++)
		{
			var simulationTime = i / (float) resolution * time;
			var displacement = initialVelocity * simulationTime + Vector3.up * (Gravity * simulationTime * simulationTime) / 2f;
			var drawPoint = startPosition + displacement;
			if (i < resolution) _trajectoryPath[i - 1] = drawPoint;
				
			Debug.DrawLine(previousDrawPoint, drawPoint, Color.Lerp(Color.red, Color.yellow, i / (float) resolution));
			previousDrawPoint = drawPoint;
		}
			
		_line.positionCount = trajectoryResolution - 1;
		_line.SetPositions(_trajectoryPath);

		if(hitInfo.collider.CompareTag("Ground"))
			hitMarker.position = hitInfo.point + hitInfo.normal * 0.01f;
		
		if(hitInfo.collider.CompareTag("TargetEnemy"))
			hitMarker.position = hitInfo.point + hitInfo.normal * 0.1f;
		
		if(hitInfo.collider.CompareTag("ExplosiveBarrel"))
			hitMarker.position = hitInfo.point + hitInfo.normal * 0.1f;
		
		hitMarker.rotation = Quaternion.LookRotation(hitInfo.normal);
	}
	
	
}
