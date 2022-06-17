
using DG.Tweening;
using UnityEngine;

public class EnemyBodyCollider : MonoBehaviour
{
	[SerializeField] private float damage;

	private EnemyRefbank _my;

	//soacho kya me sahi karra hu ye...............
	public float Damage => damage;

	private void Start()
	{
		_my = transform.root.GetComponent<EnemyRefbank>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Arrow"))
		{
			print("Give damage");
			WeaponEvents.InvokeArrowCollisonWithTargetDone();
			other.collider.GetComponent<Rigidbody>().isKinematic = true;
			other.collider.transform.parent = transform;
			other.collider.enabled = false;
			_my.Controller.GetHit(damage);
		}
	}
}
