using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour,IStickable,IExplodDamageable
{
	public bool OnStick(GameObject bomb, Transform targetTransform)
	{
		return true;
	}

	public bool OnExplodeDamage()
	{
		return true;
		
	}
}
