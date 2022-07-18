
using UnityEngine;

public class GroundController : MonoBehaviour,IStickable,IExplodDamageable
{
	
	//bro zyada complicated nahi hogaya ye?,pucho apne aap se ye........................
	[SerializeField] private IStickable.StickableBehaviour stickingBehaviour;
	[SerializeField] private IExplodDamageable.ExplodableBehaviour explodBehaviour;
	public bool OnStick(GameObject bomb,Transform target)
	{
		if (stickingBehaviour != IStickable.StickableBehaviour.Stickable) return false;
		var bombController = bomb.GetComponent<BombController>();
		if(bombController==null) return false;
		bombController.myParent = gameObject;
		bombController.disableJiggleOnGround();
		return true;
	}

	public bool OnExplodeDamage(GameObject bomb)
	{
		if (explodBehaviour != IExplodDamageable.ExplodableBehaviour.Explodable) return false;
		return true;
	}
}
