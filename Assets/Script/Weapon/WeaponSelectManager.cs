using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponSelectManager : MonoBehaviour
{
	public enum Weapon
	{ 
		Bomb,
		Arrow
	}

	public Weapon CurrentWeapon;

	private void Start()
	{
		CurrentWeapon = Weapon.Bomb;
	}
}
