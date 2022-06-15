using UnityEngine;

public class ArrowCollisonDetection : MonoBehaviour
{
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
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
	}
}
