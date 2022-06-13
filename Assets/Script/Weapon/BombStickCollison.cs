using UnityEngine;
using DG.Tweening;

public class BombStickCollison : MonoBehaviour
{
	private bool _onEnemy;
	private EnemyRefbank _enemy;
	
	private Rigidbody _rb;

	public bool OnEnemy
	{
		get => _onEnemy;
		private set => _onEnemy = value;
	}

	public EnemyRefbank Enemy
	{
		get => _enemy;
		private set => _enemy = value;
	}

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("TargetEnemy"))
		{
			_rb.isKinematic = true;
			OnEnemy = true;
			var enemyTransform = other.collider.transform;
			//iska kuch socho bhut he zyada dependancys hai...
			_enemy = enemyTransform.root.GetComponent<EnemyRefbank>();
			_enemy.Animations.GetHit();
			_enemy.Controller.AddBomb(gameObject);
			transform.parent = enemyTransform;
		}
	}
}
