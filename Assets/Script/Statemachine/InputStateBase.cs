using UnityEngine;

public class InputStateBase
{
    protected static float MaxRayDistance;
	protected static PlayerRefBank Player;
	
	
	
	protected InputStateBase() { }
	
	public InputStateBase(PlayerRefBank player)
	{
		Player = player;
		
		MaxRayDistance = LevelFlowController.only.maxRayDistance;
	}
	
	public virtual void OnEnter()
	{
			
	}

	public virtual void Execute() { }

	public virtual void FixedExecute() { }

	public virtual void OnExit() { }

	public static void print(object message) => Debug.Log(message);
	
	
}

public sealed class IdleState : InputStateBase
{
	public override void OnEnter()
	{
		print("In idle");
	}
}
