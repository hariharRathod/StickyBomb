
using UnityEngine;

public class BombShootProjectileController : MonoBehaviour
{
	private Rigidbody _rb;
	private bool _iCameFromIncrementGate;
	
	public bool ICameFromIncrementGate
	{
		get => _iCameFromIncrementGate;
		set => _iCameFromIncrementGate = value;
	}
	

	public void ShootBombInProjectile(int shootSpeedMin,int shootSpeedMax)
	{
		_rb = GetComponent<Rigidbody>();
		var shootSpeed = Random.Range(shootSpeedMin, shootSpeedMax);
		_rb.AddRelativeForce(Vector3.forward * shootSpeed);
		_iCameFromIncrementGate = true;
		
	}
}
