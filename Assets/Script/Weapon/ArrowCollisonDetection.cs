using UnityEngine;

public class ArrowCollisonDetection : MonoBehaviour
{
	private Rigidbody rb;
	private TrailRenderer _trailRenderer;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		_trailRenderer = GetComponent<TrailRenderer>();
	}
	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Bomb"))
		{
			print("ArrowCollision: " + other.collider.gameObject.name);
			rb.isKinematic = true;
			transform.parent = other.collider.transform;
		}

		if (other.collider.CompareTag("Ground"))
		{
			rb.isKinematic = true;
		}
		
		WeaponEvents.InvokeArrowCollisionWithObjects();
		_trailRenderer.enabled = false;

	}
}
