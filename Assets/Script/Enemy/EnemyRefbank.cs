using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRefbank : MonoBehaviour
{
	public bool isDead, throwBackOnLastHit;
	public int area = 0;

	public EnemyRagdollController RagdollController { get; private set; }
	public EnemyController Controller { get; private set; }

	public EnemyAnimations Animations { get; private set; }

	public EnemyMovement Movement { get; private set; }

	private void Start()
	{
		RagdollController = GetComponent<EnemyRagdollController>();
		Controller = GetComponent<EnemyController>();
		Animations = GetComponent<EnemyAnimations>();
		Movement = GetComponent<EnemyMovement>();
	}
}
