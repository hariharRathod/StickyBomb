using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class WeaponSelectManager : MonoBehaviour
{

	private PlayerRefBank _my;
	public enum Weapon
	{ 
		Bomb,
		Arrow
	}

	public Weapon currentWeapon;

	//ye yaha mat do.........
	private void OnEnable()
	{
		WeaponEvents.WeaponChangeEvent += WeaponChange;
	}

	private void OnDisable()
	{
		WeaponEvents.WeaponChangeEvent -= WeaponChange;
	}

	private void Start()
	{
		_my = GetComponent<PlayerRefBank>();
		currentWeapon = Weapon.Bomb;
		OnWeaponChange();
	
	}
	
	//isko refactor ka socho imp..............
	public void WeaponChange()
	{
		if (currentWeapon == Weapon.Bomb)
		{
			currentWeapon = Weapon.Arrow;
			
		}else if (currentWeapon == Weapon.Arrow)
		{
			currentWeapon = Weapon.Bomb;
			
		}
		OnWeaponChange();

	}

	public void OnWeaponChange()
	{
		switch (currentWeapon)
		{
			case Weapon.Bomb:
			{
				WeaponEvents.InvokeOnBombSelectEvent();
			} break;
			case Weapon.Arrow:
			{
				WeaponEvents.InvokeOnArrowSelectEvent();
			} break;
			
		}
	}

}
