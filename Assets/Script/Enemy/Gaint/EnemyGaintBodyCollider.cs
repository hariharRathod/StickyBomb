using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGaintBodyCollider : MonoBehaviour
{
	private EnemyGaintRefBank _my;

	private void Start()
	{
		_my = transform.root.GetComponent<EnemyGaintRefBank>();
	}


	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Arrow"))
		{
			
			print("Give damage");
			WeaponEvents.InvokeArrowCollisonWithTargetDone();

			if (!other.collider.TryGetComponent(out ArrowController arrowController)) return;
			
			arrowController.OnArrowCollisonWithEnemy(this.transform);
			_my.GaintController.ArrowDamage();
		}
	}
}
