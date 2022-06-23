using UnityEngine;

public class SplineTriggerController : MonoBehaviour
{
	public void StopFollowing()
	{
		print("Invoke reach next area");
		GameEvents.InvokeOnReactNextArea();
	}
}
