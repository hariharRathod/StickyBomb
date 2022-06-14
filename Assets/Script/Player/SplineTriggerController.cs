using UnityEngine;

public class SplineTriggerController : MonoBehaviour
{
	public void StopFollowing() => GameEvents.InvokeOnReactNextArea();
}
