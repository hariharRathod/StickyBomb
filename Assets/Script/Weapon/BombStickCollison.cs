using UnityEngine;
using DG.Tweening;

public class BombStickCollison : MonoBehaviour
{
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("TargetEnemy"))
		{
			rb.isKinematic = true;
		}
	}
}
