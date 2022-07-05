
using UnityEngine;

public class AimingState : InputStateBase
{
	
	private static AimController _aimer;
	private static bool _hasTarget;
	private static float _screenPercentageOnY;
	
	
	public AimingState(AimController aimer)
	{
		_aimer = aimer;
		_screenPercentageOnY = _aimer.screenPercentageOnY;
	}
	
	
	public override void OnEnter()
	{
		print("In aim state");
		base.OnEnter();
		IsPersistent = false;
		_aimer.SetReticleStatus(true);
		
	}


	public override void Execute()
	{
		base.Execute();
		_aimer.Aim(InputExtensions.GetInputDelta());
		
		var ray = Player.Camera.ScreenPointToRay(InputExtensions.GetCenterOfScreen(_screenPercentageOnY));

		if (!Physics.Raycast(ray, out var hit, MaxRayDistance))
		{
			_aimer.LoseTarget();
			/*Player.ArrowShoot.ArrowAim(hit,hit.point);
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
					if (Camera.main != null)
						Player.ArrowShoot.ShootAnyWhere(MaxRayDistance * Camera.main.transform.forward);
				}
				InputHandler.AssignNewState(InputState.Idle);
			}*/
			return;
		}
		
		if (!hit.collider.CompareTag("TargetEnemy") && !hit.collider.CompareTag("ExplosiveBarrel") && 
			!hit.collider.CompareTag("ShieldSurface") && !hit.collider.CompareTag("Bomb") && 
			!hit.collider.CompareTag("Ground") && !hit.collider.CompareTag("IncrementGate") && 
			!hit.collider.CompareTag("Props") && !hit.collider.CompareTag("Hostage") && !hit.collider.CompareTag("DeviateGate"))
		{
			_aimer.LoseTarget();
			return;
		}

		if (hit.collider.TryGetComponent(out EnemyRefbank enemyRefbank))
		{
			if(enemyRefbank.area!=LevelFlowController.only.currentArea)
			{
				_aimer.LoseTarget();
				return;
			}
		}
		
		_aimer.FindTarget();
		
		if (Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Bomb)
		{
			//on bomb aim
			
		}
		else if(Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Arrow)
		{
			print("Arrow aim");
			Player.ArrowShoot.ArrowAim(hit,hit.point);
			Player.CameraController.ZoomAction();
			
		}


		if (InputExtensions.GetFingerUp())
		{
			if (Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Bomb)
			{
				//need to think of some cooldown mechanism that will make game smooth.//soachna par iske bare me pakka..
				Player.BombThrower.Shoot(hit.transform, hit.point);
				GameEvents.InvokeOnBombRelease();
				
			}
			else if(Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Arrow)
			{
				//need to think of some cooldown mechanism that will make game smooth.//soachna par iske bare me pakka..
				Player.ArrowShoot.Shoot(hit.transform,hit.point);
				Player.CameraController.ZoomNormal();
				
			}
			
			GameEvents.InvokeOnFingerUp();
			InputHandler.AssignNewState(InputState.Idle);
		}
		
	}


	public override void OnExit()
	{
		base.OnExit();
	}
}
