
using DG.Tweening;
using UnityEngine;

public class ArrowShootMechanic : MonoBehaviour
{
	[SerializeField] private GameObject bombPrefab;
	[SerializeField] private Transform startPoint;

	private PlayerRefBank _my;
	private bool Shooted;

	private void Start()
	{
		_my = GetComponent<PlayerRefBank>();
	}

	public void ArrowAim()
	{

		_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowAim,true);
		_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowShoot,false);

	}

	public void shootArrow()
	{
		_my.PlayerAnimation.Anim.SetBool(PlayerAnimations.ArrowShoot,true);
		DOVirtual.DelayedCall(0.5f, () => _my.WeaponSelect.Arrow.SetActive(true));

	}


	public void OnShootAnimation()
	{
		_my.WeaponSelect.Arrow.SetActive(false);
	}
}
