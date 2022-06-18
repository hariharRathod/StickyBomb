using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PropExplosion : MonoBehaviour
{
	[SerializeField] private bool drawDebugSphere;

	[Header("Explosion"), SerializeField] private GameObject particleFx;
	[SerializeField] private Transform explosionOrigin;
	[SerializeField] private float explosionRadius;


	private readonly Collider[] _overlaps = new Collider[100];
	private readonly List<Transform> _affected = new List<Transform>();

	private void OnDrawGizmos()
	{
		if (!drawDebugSphere) return;
		Gizmos.color = new Color(1f, 0f, 0f, 0.44f);
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}

	private void Start()
	{
		particleFx.SetActive(false);
	}

	public void Explode()
	{
		particleFx.SetActive(true);
		particleFx.transform.parent = null;
		
		var size = Physics.OverlapSphereNonAlloc(explosionOrigin.position, explosionRadius, _overlaps);
			
		if(size == 0) return;
		
		for (var i = 0; i < size; i++)
		{
			//collu kya mast name hai bhai.................
			var collu = _overlaps[i];
				
			if(_affected.Contains(collu.transform.root)) continue;
			if(!collu.transform.root.CompareTag("TargetEnemy")) continue;

			if (collu.transform.root.TryGetComponent(out EnemyRefbank enemy))
			{
				var direction = transform.position - enemy.transform.position;
				direction.y = 0f;
				enemy.transform.DORotateQuaternion(Quaternion.LookRotation(direction), 0.01f)
					.OnComplete(() => 
						enemy.Controller.DieFromBombExplosion(true));
			}
			_affected.Add(collu.transform.root);
		}
	}

}
