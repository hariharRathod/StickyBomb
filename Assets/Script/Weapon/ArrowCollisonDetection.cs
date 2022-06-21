using UnityEngine;

public class ArrowCollisonDetection : MonoBehaviour
{
	private Rigidbody _rb;
	private TrailRenderer _trailRenderer;
	private ArrowShootProjectileController _projectileController;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_trailRenderer = transform.GetComponentInChildren<TrailRenderer>();
		_projectileController = GetComponent<ArrowShootProjectileController>();
	}
	private void OnCollisionEnter(Collision other)
	{
		
		if (other.collider.CompareTag("Bomb"))
		{
			
			print("ArrowCollision: " + other.collider.gameObject.name);
			_projectileController.ICameFromIncrementGate = false;
			_rb.isKinematic = true;
			transform.parent = other.collider.transform;
		}

		if (other.collider.CompareTag("Ground"))
		{
			_projectileController.ICameFromIncrementGate = false;
			_rb.isKinematic = true;
		}

		if (other.transform.parent)
		{
			if (other.transform.parent.TryGetComponent(out ShieldController parentShieldController))
			{
				transform.parent = other.transform;
				_projectileController.ICameFromIncrementGate = false;
				_rb.isKinematic = true;
			}

			
		}

		if (other.transform.TryGetComponent(out ShieldController shieldController))
		{
			transform.parent = other.transform;
			_projectileController.ICameFromIncrementGate = false;
			_rb.isKinematic = true;
		}


		if (other.collider.CompareTag("Player")) return;
		WeaponEvents.InvokeArrowCollisionWithObjects();
		_trailRenderer.enabled = false;

	}
}
