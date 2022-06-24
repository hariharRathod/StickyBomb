
using UnityEngine;

public class AimingState : InputStateBase
{
	
	private static AimController _aimer;
	private RaycastHit _hit;
	private static bool _hasTarget;
	private static float _screenPercentageOnY;
	
	
	public AimingState(AimController aimer)
	{
		_aimer = aimer;
		_screenPercentageOnY = _aimer.screenPercentageOnY;
	}
	
	
	public override void OnEnter()
	{
		base.OnEnter();
	}


	public override void Execute()
	{
		base.Execute();
		_aimer.Aim(InputExtensions.GetInputDelta());
		
		var ray = Player.Camera.ScreenPointToRay(InputExtensions.GetCenterOfScreen(_screenPercentageOnY));

		if (!Physics.Raycast(ray, out var hit, MaxRayDistance)) {return;}
		
		if (Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Bomb)
		{
			//on bomb aim
			
		}
		else if(Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Arrow)
		{
			print("Arrow aim");
			Player.ArrowShoot.ArrowAim(hit,hit.point);
		}


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
		}
		
	}


	public override void OnExit()
	{
		base.OnExit();
	}
}
