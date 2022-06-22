using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowController : MonoBehaviour
{
	[SerializeField] private ParticleSystem targetHitEffect;

	

	private void Start()
	{
		
	}

	public void OnArrowCollisonWithEnemy(Transform enemy)
	{
		EnableParticleHitEffect();
		GetComponent<ArrowShootProjectileController>().ICameFromIncrementGate = false;
		GetComponent<Rigidbody>().isKinematic = true;
		transform.parent = enemy;
		GetComponent<MeshCollider>().enabled = false;
	}

	public void EnableParticleHitEffect()
	{
		targetHitEffect.gameObject.SetActive(true);
	}

	

}
