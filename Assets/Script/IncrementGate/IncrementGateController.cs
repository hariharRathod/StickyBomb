using TMPro;
using UnityEngine;

public enum GateType
{
	Add,
	Multiply
}

public class IncrementGateController : MonoBehaviour
{
	
	[SerializeField] private GateType gateType;
	[SerializeField] private int number;
	[SerializeField] private TextMeshPro numberText;

	private PlayerRefBank _playerRefBank;

	
	private void Start()
	{
		_playerRefBank = GameObject.FindGameObjectWithTag("PlayerRoot").GetComponent<PlayerRefBank>();
		switch (gateType)
		{
			case GateType.Add:
			{
				numberText.text = "+ " + number.ToString();
			}
				break;
			case GateType.Multiply:
			{
				numberText.text = "X " + number.ToString();
			}
				break;
		}
		
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Arrow"))
		{
			_playerRefBank.ArrowShoot.ShootMutipleArrows(number,gateType,other.transform,transform);
			other.gameObject.SetActive(false);
		}

		if (other.CompareTag("Bomb"))
		{
			_playerRefBank.BombThrower.ShootMultipleBombs(number,gateType,other.transform,transform);
			other.gameObject.SetActive(false);
		}
		
		
	}
}
