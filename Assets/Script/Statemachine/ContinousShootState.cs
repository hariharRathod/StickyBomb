using DG.Tweening;
using UnityEngine;

public class ContinousShootState : InputStateBase
{
	private static AimController _aimer;
	private static bool _hasTarget;
	private static float _screenPercentageOnY;
	
	
	public ContinousShootState(AimController aimer)
	{
		_aimer = aimer;
		_screenPercentageOnY = _aimer.screenPercentageOnY;
	}
	
	public override void OnEnter()
	{
		print("In continous state");
		base.OnEnter();
		IsPersistent = false;
		_aimer.SetReticleStatus(true);
		
	}

	public override void Execute()
	{
		base.Execute();
		
		print("Continous shoot state");
		
		_aimer.Aim(InputExtensions.GetInputDelta());
		
		var ray = Player.Camera.ScreenPointToRay(InputExtensions.GetCenterOfScreen(_screenPercentageOnY));

		if (!Physics.Raycast(ray, out var hit, MaxRayDistance))
		{
			Player.ArrowShoot.ShootContinousArrows(ray.origin + ray.direction * MaxRayDistance);
			return;
		}

		if (hit.collider.CompareTag("Hostage"))
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
			//need to think of some cooldown mechanism that will make game smooth.//soachna par iske bare me pakka..
			Player.BombThrower.Shoot(hit.transform, hit.point);
			GameEvents.InvokeOnBombRelease();
			
		}
		else if(Player.WeaponSelect.currentWeapon == WeaponSelectManager.Weapon.Arrow)
		{
			//need to think of some cooldown mechanism that will make game smooth.//soachna par iske bare me pakka..
			
			Player.ArrowShoot.ShootContinousArrows(hit.point);
			
			/*if (Camera.main != null)
				Player.ArrowShoot.ShootContinousArrows(MaxRayDistance * Camera.main.transform.forward);*/
		}

		if (InputExtensions.GetFingerUp())
		{
			_aimer.SetReticleStatus(false);
			_aimer.ResetRotation();
		}
		
	}
}
