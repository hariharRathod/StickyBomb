using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] private Button WeaponSelect;

	public void onWeaponSelect()
	{
		WeaponEvents.InvokeWeaponActivate();
	}
}
