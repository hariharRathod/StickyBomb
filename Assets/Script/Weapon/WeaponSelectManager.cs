using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponSelectManager : MonoBehaviour
{

	[SerializeField] private GameObject bowHolder, bombHolder,arrow;
	
	private PlayerRefBank _my;
	public enum Weapon
	{ 
		Bomb,
		Arrow
	}

	public GameObject Arrow
	{
		get => arrow;
		
	}
	

	public Weapon CurrentWeapon;

	//ye yaha mat do.........
	private void OnEnable()
	{
		WeaponEvents.WeaponActivateEvent += weaponActivate;
	}

	private void OnDisable()
	{
		WeaponEvents.WeaponActivateEvent -= weaponActivate;
	}

	private void Start()
	{
		_my = GetComponent<PlayerRefBank>();
		CurrentWeapon = Weapon.Bomb;
	
	}
	
	//isko refactor ka socho imp..............
	public void weaponActivate()
	{
		if (CurrentWeapon == Weapon.Bomb)
		{
			CurrentWeapon = Weapon.Arrow;
		}else if (CurrentWeapon == Weapon.Arrow)
		{
			CurrentWeapon = Weapon.Bomb;
		}

		switch (CurrentWeapon)
		{
			case Weapon.Bomb:
			{
				bombHolder.SetActive(true);
				_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.BombSelected,true);
				_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowSelected,false);
				
				
			} break;
			case Weapon.Arrow:
			{
				print("In arrow");
				bowHolder.SetActive(true);
				_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowSelected,true);
				_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.BombSelected,false);
			} break;
			
		}
	}

}
