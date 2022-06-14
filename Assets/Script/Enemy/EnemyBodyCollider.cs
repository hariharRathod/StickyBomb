
using UnityEngine;

public class EnemyBodyCollider : MonoBehaviour
{
	[SerializeField] private float damage;

	private EnemyRefbank _my;

	private void Start()
	{
		_my = transform.root.GetComponent<EnemyRefbank>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Arrow"))
		{
			_my.Controller.GetHit(damage);
		}
	}
}
