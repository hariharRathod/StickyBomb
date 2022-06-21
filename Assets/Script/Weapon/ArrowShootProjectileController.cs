using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowShootProjectileController : MonoBehaviour
{
	private bool _iCameFromIncrementGate;

	private Rigidbody _rb;
	private MeshCollider _collider;
	private Quaternion initialRot;


	public bool ICameFromIncrementGate
	{
		get => _iCameFromIncrementGate;
		set => _iCameFromIncrementGate = value;
	}

	private void Start()
	{
		
		//_iCameFromIncrementGate = false;
	}

	private void Update()
	{
		if (!_iCameFromIncrementGate) return;
		
		print("inside update arroowwww");
		SpinInAir();
		
	
	}


	private void SpinInAir()
	{
		print("In spin in air");
		transform.rotation = Quaternion.LookRotation(_rb.velocity) * initialRot;
		
		/*var velocity = _rb.velocity;
		float xVel = velocity.x;
		float yVel = velocity.y;
		float zVel = velocity.z;

		float combinedVel = Mathf.Sqrt(xVel * xVel + zVel * zVel);
		float angle = -1 * Mathf.Atan2(yVel, combinedVel) * 180 / Mathf.PI;

		var eulerAngles = transform.eulerAngles;
		eulerAngles = new Vector3(angle,eulerAngles.y,eulerAngles.z);
		transform.eulerAngles = eulerAngles;*/
	}


	public void ShootArrowInProjectile(int shootSpeedMin,int shootSpeedMax)
	{
		initialRot = transform.rotation;
		_rb = GetComponent<Rigidbody>();
		_collider = GetComponent<MeshCollider>();
		var shootSpeed = Random.Range(shootSpeedMin, shootSpeedMax);
		_rb.freezeRotation = true;
		_rb.AddRelativeForce(Vector3.forward * shootSpeed);
		_iCameFromIncrementGate = true;
		print("arrow shoot projectile");
		GetComponentInChildren<TrailRenderer>().time = 0.3f;
		//DOVirtual.DelayedCall(0.1f, () => _collider.enabled=true);
	}
}
