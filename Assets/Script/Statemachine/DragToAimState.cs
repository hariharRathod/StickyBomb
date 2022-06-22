
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
		Debug.Log("player camera raycast: " + Player.Camera.tag, Player.Camera);

		if (!Physics.Raycast(ray, out var hit, MaxRayDistance))
		{
			
			return;
		}
		
        //isko refactor kar bhai...............
		if(hit.transform.CompareTag("TargetEnemy"))
			if (hit.transform.root.GetComponent<EnemyRefbank>().area != LevelFlowController.only.currentArea) return;
		//iska kuch socho ke har ground ka area define na karna pade
		if(hit.transform.CompareTag("Ground"))
			if (hit.transform.GetComponent<GroundAreaDetection>().area != LevelFlowController.only.currentArea) return;
		

		if (Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Bomb)
		{
			Player.BombThrower.DrawTrajectory(hit);
			print("Draw trajectory");
		}
		else if(Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Arrow)
		{
			print("Arrow aim");
			Player.ArrowShoot.ArrowAim(hit,hit.point);
			Player.ArrowShoot.Aim(InputExtensions.GetInputDelta());
		}

		
		//Player.BombThrower.updateTrajetory(hit);
		if (InputExtensions.GetFingerUp())
		{
			if (Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Bomb)
			{
				//need to think of some cooldown mechanism that will make game smooth.//soachna par iske bare me pakka..
				Player.BombThrower.Shoot(hit.transform, hit.point);
				
			}
			else if(Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Arrow)
			{
				//need to think of some cooldown mechanism that will make game smooth.//soachna par iske bare me pakka..
				Player.ArrowShoot.Shoot(hit.transform,hit.point);
				
			}
			
			InputHandler.AssignNewState(InputState.Idle);
		}

	}

	public override void OnExit()
	{
		Player.BombThrower.HideTrajectory();
		Player.ArrowShoot.HideHitMarker();
	}
}
