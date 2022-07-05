using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombNormalCollisionDetection : MonoBehaviour
{
	private Rigidbody _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.collider.CompareTag("DeviateGate")) return;

		_rb.useGravity = true;
	}
}
