
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
			
			if (_my.BombController.myParent==null) return;

			if (!_my.BombController.myParent.TryGetComponent(out IExplodDamageable explodDamageable)) return;
			
			
			if(!explodDamageable.OnExplodeDamage(this.gameObject)) return;
			explosionParticleSystem.SetActive(true);
			WeaponEvents.InvokeOnBombExplosion();
			WeaponEvents.InvokeArrowCollisonWithTargetDone();
			
			DOVirtual.DelayedCall(0.15f, () =>
			{
				explosionParticleSystem.transform.parent = null;
				gameObject.SetActive(false);
					
			});
	
		}
	}
}
