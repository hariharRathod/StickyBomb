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


	private void Start()
	{
		Anim = transform.GetChild(0).GetComponent<Animator>();
		
	}

	
	
	
}
