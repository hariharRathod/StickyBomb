
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShieldController : MonoBehaviour,IStickable,IExplodDamageable
{
	[SerializeField] private IStickable.StickableBehaviour stickingBehaviour;
	[SerializeField] private IExplodDamageable.ExplodableBehaviour explodBehaviour;
	[SerializeField] private float explosionForce;
	[SerializeField] private float explosionRadius;
	[SerializeField] private float upwardsModifier;

	private List<GameObject> _shieldPieces;
	private List<GameObject> _bombsList;
	private Camera _camera;

	private void OnEnable()
	{
		GameEvents.TapToPlay += OnTapToPlay;
	}

	private void OnDisable()
	{
		GameEvents.TapToPlay -= OnTapToPlay;
	}

	

	private void Start()
	{
		_bombsList=new List<GameObject>();
		KinematiseAllPieces();
		GetAllPieces();
		/*_camera=Camera.main;
		if (_camera != null)
		{
			var direction = transform.root.position - _camera.transform.position;
			transform.rotation = Quaternion.LookRotation(direction);
		}*/
	}

	private void GetAllPieces()
	{
		_shieldPieces=new List<GameObject>();
		for (int i = 0; i < transform.childCount; i++)
		{
			var gameObject = transform.GetChild(i).gameObject;
			_shieldPieces.Add(gameObject);
		}
	}

	private void KinematiseAllPieces()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).TryGetComponent(out Rigidbody rb);
			rb.isKinematic = true;
		}
	}

	public bool OnStick(GameObject bomb, Transform targetTransform)
	{
		
		if (stickingBehaviour != IStickable.StickableBehaviour.Stickable) return false;
		
		print("shield on stick");
		
		var bombController = bomb.GetComponent<BombController>();
		if(bombController==null) return false;
		bombController.myParent = gameObject;
		bomb.transform.parent = targetTransform;
		AddBomb(bomb);
		return true;
	}

	public bool OnExplodeDamage(GameObject bomb)
	{
		if (explodBehaviour != IExplodDamageable.ExplodableBehaviour.Explodable) return false;

		if (bomb.TryGetComponent(out SphereCollider collider))
			collider.enabled = false;
		
		
		for (int i = 0; i < _shieldPieces.Count; i++)
		{
			_shieldPieces[i].TryGetComponent(out Rigidbody rb);
			rb.isKinematic = false;
			rb.AddExplosionForce(explosionForce,transform.position,explosionRadius,upwardsModifier,ForceMode.Impulse);
		}
		
		if(transform.root.TryGetComponent(out EnemySheildController enemySheildController))
			enemySheildController.OnShieldBroken();

		for (int i = 0; i <_shieldPieces.Count; i++)
		{
			if (_shieldPieces[i].TryGetComponent(out MeshCollider meshCollider))
				//DOVirtual.DelayedCall(0.12f,()=>meshCollider.enabled = false);
			_shieldPieces[i].transform.parent = null;
		}

		for (int i = 0; i < _bombsList.Count; i++)
		{
			_bombsList[i].SetActive(false);
		}

		return true;
		
	}
	
	private void OnTapToPlay()
	{
		transform.parent.localEulerAngles = new Vector3(250,0,0);
	}


	private void AddBomb(GameObject bomb)
	{
		_bombsList.Add(bomb);
	}


}
