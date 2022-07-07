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
		WeaponEvents.OnBombButtonPressed += OnBombButtonPressed;
		WeaponEvents.OnArrowButtonPressed += OnArrowButtonPressed;
	}

	private void OnDisable()
	{
		WeaponEvents.WeaponChangeEvent -= WeaponChange;
		WeaponEvents.OnBombButtonPressed -= OnBombButtonPressed;
		WeaponEvents.OnArrowButtonPressed -= OnArrowButtonPressed;
	}

	private void Start()
	{
		_my = GetComponent<PlayerRefBank>();
		currentWeapon = Weapon.Bomb;
		//OnWeaponChange();
		OnBombButtonPressed();
	
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
	
	
	private void OnBombButtonPressed()
	{
		currentWeapon = Weapon.Bomb;
		WeaponEvents.InvokeOnBombSelectEvent();
	}
	
	private void OnArrowButtonPressed()
	{
		currentWeapon = Weapon.Arrow;
		WeaponEvents.InvokeOnArrowSelectEvent();
	}

}
