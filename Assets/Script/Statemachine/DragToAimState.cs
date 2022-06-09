using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToAimState : InputStateBase
{
	public override void OnEnter()
	{
		print("In drag to aim state");
	}

	public override void Execute()
	{
		var ray = Player.Camera.ScreenPointToRay(InputExtensions.GetInputPosition());

		if (!Physics.Raycast(ray, out var hit, MaxRayDistance)) return;


		if (Player.WeaponSelect.CurrentWeapon == WeaponSelectManager.Weapon.Bomb)
		{
			Player.BombThrower.DrawTrajectory(hit);
		}
		else if(Player.WeaponSelect.CurrentWeapon == WeaponSelectManager.Weapon.Arrow)
		{
			Player.ArrowShoot.ArrowAim();
		}

		
		//Player.BombThrower.updateTrajetory(hit);
		if (InputExtensions.GetFingerUp())
		{
			if (Player.WeaponSelect.CurrentWeapon == WeaponSelectManager.Weapon.Bomb)
			{
				Player.BombThrower.Shoot(hit.transform, hit.point);
			}
			else if(Player.WeaponSelect.CurrentWeapon == WeaponSelectManager.Weapon.Arrow)
			{
				Player.ArrowShoot.shootArrow();
			}
			
			InputHandler.AssignNewState(InputState.Idle);
		}

	}

	public override void OnExit()
	{
		
	}
}
