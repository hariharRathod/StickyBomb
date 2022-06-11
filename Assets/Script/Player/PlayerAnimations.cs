using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
	public Animator Anim { get; private set; }
	
	public static readonly int ArrowSelected = Animator.StringToHash("ArrowSelected");
	public static readonly int BombSelected = Animator.StringToHash("BombSelected");
	public static readonly int ArrowAim = Animator.StringToHash("ArrowAim");
	public static readonly int ArrowShoot = Animator.StringToHash("ArrowShoot");
	public static readonly int Aim = Animator.StringToHash("aim");


	private void OnEnable()
	{
		WeaponEvents.OnArrowSelectEvent += OnArrowWeaponSelectedAnimations;
		WeaponEvents.OnBombSelectEvent += OnBombWeaponSelectedAnimation;
	}

	private void OnDisable()
	{
		WeaponEvents.OnArrowSelectEvent -= OnArrowWeaponSelectedAnimations;
		WeaponEvents.OnBombSelectEvent -= OnBombWeaponSelectedAnimation;
	}

	private void Awake()
	{
		Anim = transform.GetChild(0).GetComponent<Animator>();
	}

	private void Start()
	{
		
		
	}
	
	private void OnArrowWeaponSelectedAnimations()
	{
		Anim.SetBool(ArrowSelected,true);
		Anim.SetBool(BombSelected,false);
	}
	
	private void OnBombWeaponSelectedAnimation()
	{
		Anim.SetBool(BombSelected,true);
		Anim.SetBool(ArrowSelected,false);
	}

	
	
	
}
