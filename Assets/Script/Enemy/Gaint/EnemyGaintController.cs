using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGaintController : MonoBehaviour
{
	private EnemyRefbank _my;

	[SerializeField] private float onBombExplosionDamage,onArrowCollisonDamage;

	public void OnStick(GameObject bomb,Transform target)
	{
		
	}



	public void OnExplodDamage()
	{
		
	}


	public void HitDamageFromArrow()
	{
		_my.Controller.GetHit(onArrowCollisonDamage);
	}

	public void HitDamageFromBomb()
	{
		_my.Controller.GetHit(onBombExplosionDamage);
	}

}
