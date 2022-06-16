using UnityEngine;
using DG.Tweening;

public class BombStickCollison : MonoBehaviour
{

	private Rigidbody _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}
	private void OnCollisionEnter(Collision other)
	{
		/*if (other.collider.CompareTag("TargetEnemy"))
		{
			_rb.isKinematic = true;
			OnEnemy = true;
			var enemyTransform = other.collider.transform;
			//iska kuch socho bhut he zyada dependancys hai...
			_enemy = enemyTransform.root.GetComponent<EnemyRefbank>();
			_enemy.Animations.GetHit();
			_enemy.Controller.AddBomb(gameObject);
			transform.parent = enemyTransform;
		}*/

		if (!other.transform.root.TryGetComponent(out IStickable stickable)) return;
		
		print("founded Istickable");

		if (!stickable.OnStick(this.gameObject,other.collider.transform)) return;
		
		print("on stick true");

		_rb.isKinematic = true;
		
	}
}
