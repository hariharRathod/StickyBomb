using DG.Tweening;
using UnityEngine;

public class EnemyGaintRagdollController : MonoBehaviour
{
	[SerializeField] private Rigidbody[] rigidbodies;
	[SerializeField] private float regularForce, throwBackForce, upForce;

	[Header("Change color on death"), SerializeField]
	private Renderer skin;

	private Material _material;
	[SerializeField]private bool shouldTurnToGrey;
	[SerializeField] private int toChangeMatIndex;
	[SerializeField] private Color deadColor;

	private EnemyGaintRefBank _my;

	private void Start()
	{
		_my = GetComponent<EnemyGaintRefBank>();
		foreach (var rb in rigidbodies) rb.isKinematic = true;
		if (shouldTurnToGrey)
			_material = skin.materials[toChangeMatIndex];
	}
	
	public void GoRagdoll(bool getThrownBack)
	{
		print("Enemy gaint ragdoll");
		_my.GaintAnimations.SetAnimatorStatus(false);

		var direction = -transform.forward;
		foreach (var rb in rigidbodies)
		{
			rb.isKinematic = false;
			rb.AddForce(direction * (getThrownBack || _my.throwBackOnLastHit ? throwBackForce : regularForce) + Vector3.up * upForce, ForceMode.Impulse);
		}
		
		if(shouldTurnToGrey)
			_material.DOColor(deadColor, 1f);
				
	}
	
	public void UnKinematicise()
	{
		foreach (var rb in rigidbodies) rb.isKinematic = false;
	}
}
