
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
			if(_my.TryGetComponent(out EnemySheildController enemySheildController))
				if (!enemySheildController.IsSheildBroken)
					return;
			
			print("Give damage");
			WeaponEvents.InvokeArrowCollisonWithTargetDone();

			if (!other.collider.TryGetComponent(out ArrowController arrowController)) return;
			
			arrowController.OnArrowCollisonWithEnemy(this.transform);

			if (_my.transform.TryGetComponent(out EnemyRunController enemyRunController))
			{
				enemyRunController.GetHit(damage,false);
				return;
			}
			
			_my.Controller.GetHit(damage,true);
		}
	}
}
