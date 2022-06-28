using UnityEngine;

public class HandAnimationHelper : MonoBehaviour
{
	private PlayerRefBank _my;

	private void Start() => _my = GetComponentInParent<PlayerRefBank>();

	public void ThrowOnAnimation()
	{
		_my.BombThrower.ThrowOnAnimation();
		AudioManager.instance.Play("BombShoot");
		
	}

	public void ArrowShootAnimation()
	{
		_my.ArrowShoot.OnShootAnimation();
		AudioManager.instance.Play("ArrowShoot");
	}


}
