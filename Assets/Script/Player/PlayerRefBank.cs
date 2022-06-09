using UnityEngine;

public class PlayerRefBank : MonoBehaviour
{
	public Animator Anim { get; private set; }
	public Camera Camera { get; private set; }
	
	public BombThrowerMechanic BombThrower { get; private set; }

	public WeaponSelectManager WeaponSelect { get; private set; }


	private void Start()
	{
		Camera = Camera.main;
		Anim = transform.GetChild(0).GetComponent<Animator>();
		BombThrower = GetComponent<BombThrowerMechanic>();
		WeaponSelect = GetComponent<WeaponSelectManager>();
	}
}
