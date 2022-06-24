
using DG.Tweening;
using UnityEngine;

public class TapState : InputStateBase
{
	public override void OnEnter()
	{
		print("In tap state");
	}

	public override void Execute()
	{
		var ray = Player.Camera.ScreenPointToRay(InputExtensions.GetInputPosition());
		Debug.Log("player camera raycast: " + Player.Camera.tag, Player.Camera);

		if (!Physics.Raycast(ray, out var hit, MaxRayDistance))
		{
			
			return;
		}
		
		if (Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Bomb)
		{
			//need to think of some cooldown mechanism that will make game smooth.//soachna par iske bare me pakka..
			Player.BombThrower.Shoot(hit.transform, hit.point);
				
		}
		else if(Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Arrow)
		{
			//need to think of some cooldown mechanism that will make game smooth.//soachna par iske bare me pakka..
			Player.ArrowShoot.ArrowAim(hit,hit.point);
			DOVirtual.DelayedCall(0.2f,()=>Player.ArrowShoot.Shoot(hit.transform,hit.point));
				
		}
		
		InputHandler.AssignNewState(InputState.Idle);
	}

	public override void OnExit()
	{
		
	}
}
