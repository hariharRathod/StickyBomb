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


		if (Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Bomb)
		{
			Player.BombThrower.DrawTrajectory(hit);
			print("Draw trajectory");
		}
		else if(Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Arrow)
		{
			Player.ArrowShoot.ArrowAim(hit,hit.point);
			Player.ArrowShoot.Aim(InputExtensions.GetInputDelta());
		}

		
		//Player.BombThrower.updateTrajetory(hit);
		if (InputExtensions.GetFingerUp())
		{
			if (Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Bomb)
			{
				Player.BombThrower.Shoot(hit.transform, hit.point);
			}
			else if(Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Arrow)
			{
				Player.ArrowShoot.Shoot(hit.point);
			}
			
			InputHandler.AssignNewState(InputState.Idle);
		}

	}

	public override void OnExit()
	{
		
	}
}
