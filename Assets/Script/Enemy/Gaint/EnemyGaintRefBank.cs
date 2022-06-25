using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGaintRefBank : MonoBehaviour
{
	public bool isDead, throwBackOnLastHit;
	public int area = 0;

	public EnemyGaintController GaintController { get; private set; }

	public EnemyGaintRagdollController GaintRagdollController { get; private set; }
	
	public EnemyGaintAnimations GaintAnimations { get; private set; }
	
	
	public EnemyGaintMovement GaintMovement { get; private set; }
	
	



	private void Start()
	{
		GaintController = GetComponent<EnemyGaintController>();
		GaintRagdollController = GetComponent<EnemyGaintRagdollController>();
		GaintAnimations = GetComponent<EnemyGaintAnimations>();
		GaintMovement = GetComponent<EnemyGaintMovement>();
	}
}
