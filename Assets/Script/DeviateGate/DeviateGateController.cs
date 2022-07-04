using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public enum DeviateGateType
{
	RightLeft,
	CenterLeftRight,
	centerRight,
}

public class DeviateGateController : MonoBehaviour
{

	[SerializeField] private DeviateGateType deviateGateType;
	[SerializeField] private GameObject arrowPrefab,bombPrefab;
	[SerializeField] private Transform rightSpwanPos, LeftSpawnPos;
	[SerializeField] private float rightLeftGateMaxDistBeforeRotate,arrowSpeed,bombSpeed;
	

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
		{
			switch (deviateGateType)
			{
				case DeviateGateType.RightLeft:
					DeviateRightLeftArrow(other.gameObject);
					break;
				case DeviateGateType.centerRight:
					break;
				case DeviateGateType.CenterLeftRight:
					break;
				
			}
			
		}


		if (other.CompareTag("Bomb"))
		{
			switch (deviateGateType)
			{
				case DeviateGateType.RightLeft:
					DeviateRightLeftBomb(other.gameObject);
					break;
				case DeviateGateType.centerRight:
					break;
				case DeviateGateType.CenterLeftRight:
					break;
				
			}
		}
			
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
				bomb.transform.position = rightSpwanPos.position;
				rightBomb.transform.DORotate(Vector3.zero, 0.01f).OnComplete(() =>
				{
					rightBomb.transform.DORotate(new Vector3(0,25f,0), 0.01f).SetEase(Ease.Linear).OnComplete(()=>
					{
						var rb=rightBomb.GetComponent<Rigidbody>();
						rb.useGravity = false;
						//rb.AddRelativeForce(rightArrow.transform.forward * arrowSpeed);
						rb.AddForce(rightBomb.transform.forward * bombSpeed);
						rightBomb.GetComponentInChildren<TrailRenderer>().time = 0.3f;

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
				leftBomb.transform.position = LeftSpawnPos.position;
				leftBomb.transform.DORotate(Vector3.zero, 0.01f).OnComplete(() =>
				{
					leftBomb.transform.DORotate(new Vector3(0,-25f,0), 0.01f).SetEase(Ease.Linear).OnComplete(() =>
					{
						var rb=leftBomb.GetComponent<Rigidbody>();
						rb.useGravity = false;
						//rb.AddRelativeForce(leftArrow.transform.forward * arrowSpeed);
						rb.AddForce(leftBomb.transform.forward * bombSpeed);
						leftBomb.GetComponentInChildren<TrailRenderer>().time = 0.3f;
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
				rightArrow.transform.position = rightSpwanPos.position;
				rightArrow.transform.DORotate(Vector3.zero, 0.01f).OnComplete(() =>
				{
					rightArrow.transform.DORotate(new Vector3(0,25f,0), 0.01f).SetEase(Ease.Linear).OnComplete(()=>
					{
						var rb=rightArrow.GetComponent<Rigidbody>();
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
				leftArrow.transform.position = LeftSpawnPos.position;
				leftArrow.transform.DORotate(Vector3.zero, 0.01f).OnComplete(() =>
				{
					leftArrow.transform.DORotate(new Vector3(0,-25f,0), 0.01f).SetEase(Ease.Linear).OnComplete(() =>
					{
						var rb=leftArrow.GetComponent<Rigidbody>();
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
