using UnityEngine;

public class PlayerRefBank : MonoBehaviour
{
	 
	public PlayerAnimations PlayerAnimation {get; private set;}
	public Camera Camera { get; private set; }

	public BombThrowerMechanic BombThrower { get; private set; }

	public WeaponSelectManager WeaponSelect { get; private set; }

	public ArrowShootMechanic ArrowShoot { get; private set; }

	public WeaponSelectCanvas WeaponCanvas { get; private set; }

	public PlayerController Controller { get; private set; }




	private void Start()
	{
		Camera = Camera.main;
		Debug.Log("player camera raycast: " + Camera.tag, Camera);
		PlayerAnimation = GetComponent<PlayerAnimations>();
		Controller = GetComponent<PlayerController>();
		BombThrower = GetComponent<BombThrowerMechanic>();
		ArrowShoot = GetComponent<ArrowShootMechanic>();
		WeaponSelect = GetComponent<WeaponSelectManager>();
		WeaponCanvas = GameObject.FindGameObjectWithTag("WeaponSelectCanvas").GetComponent<WeaponSelectCanvas>();
	}
}
