using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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
	private Transform _targetTransform;

	private void Start()
	{
		_arrowsFromDeviateGate=new List<GameObject>();
		_bombFromDeviateGate=new List<GameObject>();
		_transform = transform;
		_middleTransform = middleGameObject.transform;
		_targetTransform = GameObject.FindGameObjectWithTag("TargetEnemy").transform;
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

		if (!_targetTransform) return;
		
		var myPosition = _transform.position;
		var bomb = Instantiate(bombPrefab, myPosition, Quaternion.identity);
		_bombFromDeviateGate.Add(bomb);
		_middleTransform.rotation = Quaternion.Euler(0,0,0);
		

		var rb = bomb.GetComponent<Rigidbody>();
		if(!useGravityForArrow)
			rb.useGravity = false;
		
		Debug.DrawLine(_transform.position,_targetTransform.position ,Color.red,2f,false);
		var targetTransform = _targetTransform;
		Debug.Log(targetTransform.name, targetTransform);
		var middlePoint = (targetTransform.position - myPosition) * 0.5f;
		middlePoint.y = 0f;
		_middleTransform.position = myPosition + middlePoint;

		bomb.transform.parent = _middleTransform;

		var bombInitialAngle = Quaternion.Euler(0, bombRightAngle, 0);
		bomb.transform.rotation = bombInitialAngle;
		_middleTransform.DORotate(new Vector3(0, middleRotateEndValue, 0), 1f).SetEase(Ease.Linear).OnComplete(() =>
		{
			//arrow.transform.parent = null;
			rb.AddForce(bomb.transform.forward * bombSpeed);
			_middleTransform.rotation = Quaternion.Euler(0,0,0);
		});
		initialBomb.SetActive(false);
	}

	private void DeviateCircularArrow(GameObject initialArrow)
	{
		if (_arrowsFromDeviateGate.Contains(initialArrow)) return;
		
		if (!_targetTransform) return;
		
		var myPosition = _transform.position;
		var arrow = Instantiate(arrowPrefab, myPosition, Quaternion.identity);
		_arrowsFromDeviateGate.Add(arrow);
		_middleTransform.rotation = Quaternion.Euler(0,0,0);

		var arrowRb = arrow.GetComponent<Rigidbody>();
		if(!useGravityForArrow)
			arrowRb.useGravity = false;
		
		Debug.DrawLine(_transform.position,_targetTransform.position, Color.red,2f,false);
		var targetTransform = _targetTransform;
		Debug.Log(targetTransform.name, targetTransform);
		var middlePoint = (targetTransform.position - myPosition) * 0.5f;
		middlePoint.y = 0f;
		_middleTransform.position = myPosition + middlePoint;

		arrow.transform.parent = _middleTransform;

		var arrowInitialAngle = Quaternion.Euler(0, arrowRightAngle, 0);
		arrow.transform.rotation = arrowInitialAngle;
		_middleTransform.DORotate(new Vector3(0, middleRotateEndValue, 0), 1f).SetEase(Ease.Linear).OnComplete(() =>
		{
			//arrow.transform.parent = null;
			arrowRb.AddForce(arrow.transform.forward * arrowSpeed);
			_middleTransform.rotation = Quaternion.Euler(0,0,0);
		});
		initialArrow.SetActive(false);


	}
}
