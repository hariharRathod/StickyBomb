
using System.Collections.Generic;
using UnityEngine;

public class PropsController : MonoBehaviour,IStickable,IExplodDamageable
{
	private Rigidbody _rb;
	
	[SerializeField] private IStickable.StickableBehaviour stickingBehaviour;
	[SerializeField] private IExplodDamageable.ExplodableBehaviour explodBehaviour;
	[SerializeField] private bool shouldThrownBack;

	[SerializeField] private float throwBackForce, upForce;
	private List<GameObject> _bombsList;

	private void OnEnable()
	{
		EnemyEvents.GaintBombCollison += OnGaintBombCollison;
	}

	private void OnDisable()
	{
		EnemyEvents.GaintBombCollison -= OnGaintBombCollison;
	}

	private void Start()
	{
		_bombsList=new List<GameObject>();
		_rb = GetComponent<Rigidbody>();
		_rb.isKinematic = true;
	}

	public void OnArrowCollison()
	{
		print("prop arrow collison");
		UnKinematiseProp();
		if (!shouldThrownBack) return;
		
		ThrowBackProps();

	}
	
	public void disableAllBombsOnMe()
	{
		foreach (var bomb in _bombsList)
		{
			bomb.SetActive(false);
		}
	}
	
	public void AddBomb(GameObject bomb)
	{
		_bombsList.Add(bomb);
	}

	public bool OnStick(GameObject bomb, Transform targetTransform)
	{
		
		if (stickingBehaviour != IStickable.StickableBehaviour.Stickable) return false;
		
		var bombController = bomb.GetComponent<BombController>();
		if(bombController==null) return false;
		bombController.myParent = gameObject;
		bombController.IAmOnEnemy = true;
		AddBomb(bomb);
		bomb.transform.parent = targetTransform;
		
		return true;
	}

	public bool OnExplodeDamage(GameObject bomb)
	{
		if (explodBehaviour != IExplodDamageable.ExplodableBehaviour.Explodable) return false;

		UnKinematiseProp();
		
		return true;
	}

	public  void UnKinematiseProp()
	{
		
		transform.parent = null;
		_rb.isKinematic = false;
		disableAllBombsOnMe();
	}

	public void ThrowBackProps()
	{
		UnKinematiseProp();
		print("prop throw back");
		_rb.AddForce(_rb.transform.up * upForce,ForceMode.Impulse);
	}
	
	private void OnGaintBombCollison()
	{
		if (!_rb.isKinematic) return;
		
		UnKinematiseProp();
		ThrowBackProps();
	}
}
