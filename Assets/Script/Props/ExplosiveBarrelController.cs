using DG.Tweening;
using UnityEngine;

public class ExplosiveBarrelController : MonoBehaviour,IStickable,IExplodDamageable
{
	
	[SerializeField] private IStickable.StickableBehaviour stickingBehaviour;
	[SerializeField] private IExplodDamageable.ExplodableBehaviour explodBehaviour;
	
	public bool OnStick(GameObject bomb, Transform targetTransform)
	{
		if (stickingBehaviour != IStickable.StickableBehaviour.Stickable) return false;
		
		var bombController = bomb.GetComponent<BombController>();
		if(bombController==null) return false;
		bombController.myParent = gameObject;
		bomb.transform.parent = targetTransform;
		return true;
	}

	public bool OnExplodeDamage()
	{
		if (explodBehaviour != IExplodDamageable.ExplodableBehaviour.Explodable) return false;
		DOVirtual.DelayedCall(0.15f, ()=>gameObject.SetActive(false));
		return true;
	}
}
