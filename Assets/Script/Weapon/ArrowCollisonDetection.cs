using UnityEngine;

public class ArrowCollisonDetection : MonoBehaviour
{
	private Rigidbody _rb;
	private TrailRenderer _trailRenderer;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_trailRenderer = transform.GetComponentInChildren<TrailRenderer>();
	}
	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Bomb"))
		{
			print("ArrowCollision: " + other.collider.gameObject.name);
			_rb.isKinematic = true;
			transform.parent = other.collider.transform;
		}

		if (other.collider.CompareTag("Ground"))
		{
			_rb.isKinematic = true;
		}

		if (other.transform.parent)
		{
			if (other.transform.parent.TryGetComponent(out ShieldController parentShieldController))
			{
				transform.parent = other.transform;
				_rb.isKinematic = true;
			}

			
		}

		if (other.transform.TryGetComponent(out ShieldController shieldController))
		{
			transform.parent = other.transform;
			_rb.isKinematic = true;
		}


		if (other.collider.CompareTag("Player")) return;
		WeaponEvents.InvokeArrowCollisionWithObjects();
		_trailRenderer.enabled = false;

	}
}
