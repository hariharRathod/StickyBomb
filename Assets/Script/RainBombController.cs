using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBombController : MonoBehaviour
{
	[SerializeField] private List<Rigidbody> bombs;
	[SerializeField] private bool rainOnStart;

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
		if (rainOnStart) return;
		
		foreach (var rb in bombs)
		{
			rb.isKinematic = true;
		}
		
	}


	private void OnTapToPlay()
	{
		if (rainOnStart) return;
		
		foreach (var rb in bombs)
		{
			rb.isKinematic = false;
		}
	}
}
	
	
