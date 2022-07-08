
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DeviateCenterRightLeftController : MonoBehaviour
{
	[SerializeField] private GameObject arrowPrefab,bombPrefab;
	[SerializeField] private Transform centerSpawnPosArrow,rightSpawnPosArrow,leftSpawnPosArrow,centerSpawnPosBomb,rightSpawnPosBomb,leftSpawnPosBomb;
	[SerializeField] private float arrowSpeed,bombSpeed,rightArrowRotateAngle,leftArrowRotateAngle,leftBombRotateAngle,rightBombRotateAngle;
	[SerializeField] private bool useGravityForArrow, useGravityForBomb;

	private List<GameObject> _arrowsFromDeviateGate;
	private List<GameObject> _bombFromDeviateGate;
	
	private readonly int _centerRightLeftGateNumberOfArrows = 3;
	private readonly int _centerRightLeftGateNumberOfBombs = 3;
	
	private void Start()
	{
		_arrowsFromDeviateGate=new List<GameObject>();
		_bombFromDeviateGate=new List<GameObject>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Arrow"))
			DeviateCenterRightLeftArrow(other.gameObject);
		
		if (other.CompareTag("Bomb"))
			DeviateCenterRightLeftBomb(other.gameObject);
	
	}

	private void DeviateCenterRightLeftArrow(GameObject initialArrow)
	{
		if (_arrowsFromDeviateGate.Contains(initialArrow)) return;
		
		for (int i = 0; i < _centerRightLeftGateNumberOfArrows; i++)
		{
			
			var arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
			_arrowsFromDeviateGate.Add(arrow);
			
			if (i == 0)
			{
				var rightArrow = arrow;
				rightArrow.transform.position = rightSpawnPosArrow.position;
				rightArrow.transform.DORotate(Vector3.zero, 0.01f).OnComplete(() =>
				{
					rightArrow.transform.DORotate(new Vector3(0,rightArrowRotateAngle,0), 0.01f).SetEase(Ease.Linear).OnComplete(()=>
					{
						var rb=rightArrow.GetComponent<Rigidbody>();
						if(!useGravityForArrow)
							rb.useGravity = false;
						//rb.AddRelativeForce(rightArrow.transform.forward * arrowSpeed);
						rb.AddForce(rightArrow.transform.forward * arrowSpeed);
						rightArrow.GetComponentInChildren<TrailRenderer>().time = 0.3f;

						DOVirtual.DelayedCall(0.2f, () =>
						{
							rightArrow.transform.DORotate(Vector3.zero, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
							{
								//rb.AddRelativeForce(Vector3.forward * arrowSpeed);
								rb.AddForce(rightArrow.transform.forward * arrowSpeed);
							});
						});
					});
				});
				
			}
			else if (i == _centerRightLeftGateNumberOfArrows - 2)
			{
				var leftArrow = arrow;
				leftArrow.transform.position = leftSpawnPosArrow.position;
				leftArrow.transform.DORotate(Vector3.zero, 0.01f).OnComplete(() =>
				{
					leftArrow.transform.DORotate(new Vector3(0,leftArrowRotateAngle,0), 0.01f).SetEase(Ease.Linear).OnComplete(() =>
					{
						var rb=leftArrow.GetComponent<Rigidbody>();
						if(!useGravityForArrow)
							rb.useGravity = false;
						//rb.AddRelativeForce(leftArrow.transform.forward * arrowSpeed);
						rb.AddForce(leftArrow.transform.forward * arrowSpeed);
						leftArrow.GetComponentInChildren<TrailRenderer>().time = 0.3f;
						DOVirtual.DelayedCall(0.2f, () =>
						{
							leftArrow.transform.DORotate(Vector3.zero, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
							{
								//rb.AddRelativeForce(Vector3.forward * arrowSpeed);
								rb.AddForce(leftArrow.transform.forward * arrowSpeed);
							});
						});
					});
				});
				
			}
			else if (i == _centerRightLeftGateNumberOfArrows - 1)
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
			
		}
		initialArrow.SetActive(false);
	}

	private void DeviateCenterRightLeftBomb(GameObject initialBomb)
	{
		if (_bombFromDeviateGate.Contains(initialBomb)) return;
		
		for (int i = 0; i < _centerRightLeftGateNumberOfBombs; i++)
		{
			
			var bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
			_bombFromDeviateGate.Add(bomb);
			
			if (i == 0)
			{
				var rightBomb = bomb;
				rightBomb.transform.position = rightSpawnPosBomb.position;
				rightBomb.transform.DORotate(Vector3.zero, 0.01f).OnComplete(() =>
				{
					rightBomb.transform.DORotate(new Vector3(0,rightBombRotateAngle,0), 0.01f).SetEase(Ease.Linear).OnComplete(()=>
					{
						var rb=rightBomb.GetComponent<Rigidbody>();
						if(!useGravityForBomb)
							rb.useGravity = false;
						//rb.AddRelativeForce(rightArrow.transform.forward * arrowSpeed);
						rb.AddForce(rightBomb.transform.forward * bombSpeed);
						

						DOVirtual.DelayedCall(0.2f, () =>
						{
							rightBomb.transform.DORotate(Vector3.zero, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
							{
								//rb.AddRelativeForce(Vector3.forward * arrowSpeed);
								rb.AddForce(rightBomb.transform.forward * bombSpeed);
							});
						});
					});
				});
				
			}
			else if (i == _centerRightLeftGateNumberOfArrows - 2)
			{
				var leftBomb = bomb;
				leftBomb.transform.position = leftSpawnPosArrow.position;
				leftBomb.transform.DORotate(Vector3.zero, 0.01f).OnComplete(() =>
				{
					leftBomb.transform.DORotate(new Vector3(0,leftBombRotateAngle,0), 0.01f).SetEase(Ease.Linear).OnComplete(() =>
					{
						var rb=leftBomb.GetComponent<Rigidbody>();
						if(!useGravityForBomb)
							rb.useGravity = false;
						//rb.AddRelativeForce(leftArrow.transform.forward * arrowSpeed);
						rb.AddForce(leftBomb.transform.forward * bombSpeed);
						
						DOVirtual.DelayedCall(0.2f, () =>
						{
							leftBomb.transform.DORotate(Vector3.zero, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
							{
								//rb.AddRelativeForce(Vector3.forward * arrowSpeed);
								rb.AddForce(leftBomb.transform.forward * bombSpeed);
							});
						});
					});
				});
				
			}
			else if (i == _centerRightLeftGateNumberOfArrows - 1)
			{
				var centerBomb = bomb;
				centerBomb.transform.position = centerSpawnPosBomb.position;

				var rb =centerBomb.GetComponent<Rigidbody>();
				if(!useGravityForBomb)
					rb.useGravity = false;
				centerBomb.transform.DORotate(Vector3.zero, 0.01f).SetEase(Ease.Linear).OnComplete(() =>
				{
					rb.AddForce(centerBomb.transform.forward * bombSpeed);	
				});
			}
			
		}
		initialBomb.SetActive(false);
	}
}
