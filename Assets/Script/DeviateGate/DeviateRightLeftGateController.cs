using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DeviateRightLeftGateController : MonoBehaviour
{
	[SerializeField] private GameObject arrowPrefab,bombPrefab;
	[SerializeField] private Transform rightSpawnPosArrow, leftSpawnPosArrow,rightSpawnPosBomb,leftSpawnPosBomb;
	[SerializeField] private float arrowSpeed,bombSpeed,leftArrowRotateAngle,rightArrowRotateAngle,rightBombRotateAngle,leftBombRotateAngle;
	[SerializeField] private bool useGravityForArrow, useGravityForBomb;

	private List<GameObject> _arrowsFromDeviateGate;
	private List<GameObject> _bombFromDeviateGate;
	
	private readonly int _rightLeftGateNumberOfArrows = 2;
	private readonly int _rightLeftGateNumberOfBombs = 2;
	
	private void Start()
	{
		_arrowsFromDeviateGate=new List<GameObject>();
		_bombFromDeviateGate=new List<GameObject>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Arrow"))
			DeviateRightLeftArrow(other.gameObject);
		
		if (other.CompareTag("Bomb"))
			DeviateRightLeftBomb(other.gameObject);
	
	}

	private void DeviateRightLeftBomb(GameObject initialBomb)
	{
		if (_bombFromDeviateGate.Contains(initialBomb)) return;
		
		for (int i = 0; i < _rightLeftGateNumberOfBombs; i++)
		{
			var bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
			_bombFromDeviateGate.Add(bomb);

			if (i == 0)
			{
				var rightBomb = bomb;
				bomb.transform.position = rightSpawnPosBomb.position;
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
			else if (i == _rightLeftGateNumberOfBombs - 1)
			{
				var leftBomb = bomb;
				leftBomb.transform.position = leftSpawnPosBomb.position;
				
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
		}
		
		initialBomb.SetActive(false);
	}

	private void DeviateRightLeftArrow(GameObject initialArrow)
	{
		if (_arrowsFromDeviateGate.Contains(initialArrow)) return;
		
		for (int i = 0; i < _rightLeftGateNumberOfArrows; i++)
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
			else if (i == _rightLeftGateNumberOfArrows - 1)
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
			
		}
		initialArrow.SetActive(false);
	}
}
