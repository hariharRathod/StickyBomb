using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DeviateCenterRightController : MonoBehaviour
{
	[SerializeField] private GameObject arrowPrefab,bombPrefab;
	[SerializeField] private Transform centerSpawnPosArrow,rightSpawnPosArrow,centerSpawnPosBomb,rightSpawnPosBomb;
	[SerializeField] private float arrowSpeed,bombSpeed,rightArrowRotateAngle,rightBombRotateAngle;
	[SerializeField] private bool useGravityForArrow, useGravityForBomb;

	private List<GameObject> _arrowsFromDeviateGate;
	private List<GameObject> _bombFromDeviateGate;
	
	private readonly int _centerRightGateNumberOfArrows = 2;
	private readonly int _centerRightGateNumberOfBombs = 2;
	
	private void Start()
	{
		_arrowsFromDeviateGate=new List<GameObject>();
		_bombFromDeviateGate=new List<GameObject>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Arrow"))
			DeviateCenterRightArrow(other.gameObject);
		
		if (other.CompareTag("Bomb"))
			DeviateCenterRightBomb(other.gameObject);
	
	}

	private void DeviateCenterRightArrow(GameObject initialArrow)
	{
		if (_arrowsFromDeviateGate.Contains(initialArrow)) return;
		
		for (int i = 0; i < _centerRightGateNumberOfArrows; i++)
		{
			
			var arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
			_arrowsFromDeviateGate.Add(arrow);
			
			if (i == 0)
			{
				var centerArrow = arrow;
				centerArrow.transform.position = centerSpawnPosArrow.position;

				var rb = centerArrow.GetComponent<Rigidbody>();
				if(!useGravityForArrow)
					rb.useGravity = false;
				centerArrow.transform.DORotate(Vector3.zero, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
				{
					rb.AddForce(centerArrow.transform.forward * arrowSpeed);	
				});
				
			}
			else if (i == _centerRightGateNumberOfArrows - 1)
			{
				var rightArrow = arrow;
				rightArrow.transform.position =rightSpawnPosArrow.position;
				rightArrow.transform.DORotate(Vector3.zero, 0.01f).OnComplete(() =>
				{
					rightArrow.transform.DORotate(new Vector3(0,rightArrowRotateAngle,0), 0.01f).SetEase(Ease.Linear).OnComplete(() =>
					{
						var rb= rightArrow.GetComponent<Rigidbody>();
						if(!useGravityForArrow)
							rb.useGravity = false;
						//rb.AddRelativeForce(leftArrow.transform.forward * arrowSpeed);
						rb.AddForce( rightArrow.transform.forward * arrowSpeed);
						rightArrow.GetComponentInChildren<TrailRenderer>().time = 0.3f;
						DOVirtual.DelayedCall(0.2f, () =>
						{
							rightArrow.transform.DORotate(Vector3.zero, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
							{
								//rb.AddRelativeForce(Vector3.forward * arrowSpeed);
								rb.AddForce( rightArrow.transform.forward * arrowSpeed);
							});
						});
					});
				});
				
			}
			
		}
		initialArrow.SetActive(false);
	}

	private void DeviateCenterRightBomb(GameObject initalBomb)
	{
		if (_bombFromDeviateGate.Contains(initalBomb)) return;
		
		for (int i = 0; i < _centerRightGateNumberOfBombs; i++)
		{
			
			var bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
			_bombFromDeviateGate.Add(bomb);
			
			if (i == 0)
			{
				var centerBomb = bomb;
				centerBomb.transform.position = centerSpawnPosBomb.position;

				var rb = centerBomb.GetComponent<Rigidbody>();
				if(!useGravityForBomb)
					rb.useGravity = false;
				centerBomb.transform.DORotate(Vector3.zero, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
				{
					rb.AddForce(centerBomb.transform.forward * bombSpeed);	
				});
				
			}
			else if (i == _centerRightGateNumberOfBombs - 1)
			{
				var rightBomb= bomb;
				rightBomb.transform.position =rightSpawnPosBomb.position;
				rightBomb.transform.DORotate(Vector3.zero, 0.01f).OnComplete(() =>
				{
					rightBomb.transform.DORotate(new Vector3(0,rightBombRotateAngle,0), 0.01f).SetEase(Ease.Linear).OnComplete(() =>
					{
						var rb= rightBomb.GetComponent<Rigidbody>();
						if(!useGravityForBomb)
							rb.useGravity = false;
						//rb.AddRelativeForce(leftArrow.transform.forward * arrowSpeed);
						rb.AddForce( rightBomb.transform.forward *bombSpeed);
						
						DOVirtual.DelayedCall(0.2f, () =>
						{
							rightBomb.transform.DORotate(Vector3.zero, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
							{
								//rb.AddRelativeForce(Vector3.forward * arrowSpeed);
								rb.AddForce( rightBomb.transform.forward *bombSpeed);
							});
						});
					});
				});
				
			}
			
		}
		initalBomb.SetActive(false);
	}
}
