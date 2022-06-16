using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStickable
{
	//kya me ye sahi kiya, saochna ye......................
	enum StickableBehaviour
	{
		Stickable,
		NotStickable
	}
	//bro wtf itna complicated hai ye bro,simplify karo zara soacho soacho...................
	bool OnStick(GameObject bomb,Transform targetTransform);
}

public interface IExplodDamageable
{
	//kya me ye sahi kiya, saochna ye......................
	
	enum ExplodableBehaviour
	{
		Explodable,
		NotExplodable
		
	}
	bool OnExplodeDamage();
}
