using DG.Tweening;
using UnityEngine;

public class BombArrowCollison : MonoBehaviour
{
	[SerializeField] private GameObject explosionParticleSystem,bombMesh;

	private BombRefBank _my;
	
	private void Start()
	{
		explosionParticleSystem.SetActive(false);
		_my = GetComponent<BombRefBank>();

	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Arrow"))
		{
			
			if (_my.BombStickCollison.OnEnemy)
			{
				explosionParticleSystem.SetActive(true);
				
				DOVirtual.DelayedCall(0.15f, () =>
				{
					explosionParticleSystem.transform.parent = null;
					//iska kuch socho bhut he zyada dependancys hai...
					_my.BombStickCollison.Enemy.Controller.EnemyDie(true);
					gameObject.SetActive(false);
					
				});
				
			}
		}
	}
}
