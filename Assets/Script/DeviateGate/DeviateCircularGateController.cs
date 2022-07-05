using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviateCircularGateController : MonoBehaviour
{
	[SerializeField] private GameObject arrowPrefab,bombPrefab;
	[SerializeField] private Transform centerSpawnPosArrow,centerSpawnPosBomb;
	[SerializeField] private float arrowSpeed,bombSpeed;
	[SerializeField] private bool useGravityForArrow, useGravityForBomb;

	private List<GameObject> _arrowsFromDeviateGate;
	private List<GameObject> _bombFromDeviateGate;
	
	private readonly int _circularGateNumberOfArrows = 1;
	private readonly int _circularGateNumberOfBombs = 1;
	
	private void Start()
	{
		_arrowsFromDeviateGate=new List<GameObject>();
		_bombFromDeviateGate=new List<GameObject>();
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
		
	}

	private void DeviateCircularArrow(GameObject initialArrow)
	{
		
	}
}
