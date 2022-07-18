using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageBodyCollider : MonoBehaviour
{

	private HostageController _controller;


	private void Start()
	{
		_controller = transform.root.GetComponent<HostageController>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!other.collider.CompareTag("Arrow")) return;

		if (!_controller) return;

		if (_controller.IsDead) return;
		
		_controller.HostageDie(true);

	}
}
