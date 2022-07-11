
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DeviateCircularRightGateController : MonoBehaviour
{
	[SerializeField] private GameObject arrowPrefab,bombPrefab,middleGameObject;
	[SerializeField] private Transform centerSpawnPosArrow,centerSpawnPosBomb;
	[SerializeField] private float arrowSpeed,bombSpeed,arrowWidth,arrowHeight,arrowRightAngle,bombRightAngle,middleRotateEndValue;
	[SerializeField] private bool useGravityForArrow, useGravityForBomb;

	private List<GameObject> _arrowsFromDeviateGate;
	private List<GameObject> _bombFromDeviateGate;
	
	private Transform _transform;
	private Transform _middleTransform;
	[SerializeField] private Transform targetTransform;

	private void Start()
	{
		_arrowsFromDeviateGate=new List<GameObject>();
		_bombFromDeviateGate=new List<GameObject>();
		_transform = transform;
		_middleTransform = middleGameObject.transform;

		if (targetTransform) return;
		
		targetTransform = GameObject.FindGameObjectWithTag("TargetEnemy").transform;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Arrow"))
			DeviateCircularArrow(other.gameObject);
		
		if (other.CompareTag("Bomb"))
			DeviateCircularBomb(other.gameObject);
	
	}

	private void DeviateCircularBomb(GameObject initialBomb)
	{
		if (_bombFromDeviateGate.Contains(initialBomb)) return;

		if (!this.targetTransform) return;
		
		var myPosition = _transform.position;
		var bomb = Instantiate(bombPrefab, myPosition, Quaternion.identity);
		_bombFromDeviateGate.Add(bomb);
		_middleTransform.rotation = Quaternion.Euler(0,0,0);
		bomb.transform.localScale = Vector3.one * 22f;
		

		var rb = bomb.GetComponent<Rigidbody>();
		if(!useGravityForArrow)
			rb.useGravity = false;
		
		Debug.DrawLine(_transform.position,this.targetTransform.position ,Color.red,2f,false);
		var targetTransform = this.targetTransform;
		Debug.Log(targetTransform.name, targetTransform);
		var middlePoint = (targetTransform.position - myPosition) * 0.5f;
		middlePoint.y = 0f;
		_middleTransform.position = myPosition + middlePoint;

		bomb.transform.parent = _middleTransform;

		var bombInitialAngle = Quaternion.Euler(0, bombRightAngle, 0);
		bomb.transform.rotation = bombInitialAngle;
		GameEvents.InvokeOnCircularViewStart();
		_middleTransform.DORotate(new Vector3(0, middleRotateEndValue, 0), 1.5f).SetEase(Ease.Linear).OnComplete(() =>
		{
			//arrow.transform.parent = null;
			rb.AddForce(bomb.transform.forward * bombSpeed);
			_middleTransform.rotation = Quaternion.Euler(0,0,0);
			GameEvents.InvokeOnCircularViewEnd();
		});
		initialBomb.SetActive(false);
	}

	private void DeviateCircularArrow(GameObject initialArrow)
	{
		if (_arrowsFromDeviateGate.Contains(initialArrow)) return;
		
		if (!this.targetTransform) return;
		
		var myPosition = _transform.position;
		var arrow = Instantiate(arrowPrefab, myPosition, Quaternion.identity);
		_arrowsFromDeviateGate.Add(arrow);
		_middleTransform.rotation = Quaternion.Euler(0,0,0);

		var arrowRb = arrow.GetComponent<Rigidbody>();
		if(!useGravityForArrow)
			arrowRb.useGravity = false;
		
		Debug.DrawLine(_transform.position,this.targetTransform.position, Color.red,2f,false);
		var targetTransform = this.targetTransform;
		Debug.Log(targetTransform.name, targetTransform);
		var middlePoint = (targetTransform.position - myPosition) * 0.5f;
		middlePoint.y = 0f;
		_middleTransform.position = myPosition + middlePoint;

		arrow.transform.parent = _middleTransform;

		var arrowInitialAngle = Quaternion.Euler(0, arrowRightAngle, 0);
		arrow.transform.rotation = arrowInitialAngle;
		GameEvents.InvokeOnCircularViewStart();
		_middleTransform.DORotate(new Vector3(0, middleRotateEndValue, 0), 1.5f).SetEase(Ease.Linear).OnComplete(() =>
		{
			//arrow.transform.parent = null;
			arrowRb.AddForce(arrow.transform.forward * arrowSpeed);
			_middleTransform.rotation = Quaternion.Euler(0,0,0);
			GameEvents.InvokeOnCircularViewEnd();
		});
		
		initialArrow.SetActive(false);


	}
}
