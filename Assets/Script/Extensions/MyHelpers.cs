using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class MyHelpers
{
	private static readonly Dictionary<float, WaitForSeconds> WaitForSecondsMap = new Dictionary<float, WaitForSeconds>();

	#if UNITY_EDITOR
	[UnityEditor.MenuItem("GameObject/------Separator------ _F1", false, 0)]
	public static void CreateSeparator()
	{
		new GameObject("---------------------------");
	}
	#endif
	
	public static WaitForSeconds GetWaiter(float time)
	{
		if (WaitForSecondsMap.ContainsKey(time))
			return WaitForSecondsMap[time];

		var waiter = new WaitForSeconds(time);
		WaitForSecondsMap.Add(time, waiter);
		return waiter;
	}

	/// <summary>
	/// Returns whether the distance between given 2 points is greater than the (squared) threshold.
	/// </summary>
	/// <param name="directionVector">The (subtracted) direction vector between 2 vectors</param>
	/// <param name="squaredThreshold"> You have to pass in the desired threshold after squaring it.</param>
	/// <returns>Whether the distance is more than/ point A isn't close enough to B</returns>
	public static bool IsVectorDistanceMoreThan(this Vector3 directionVector, float squaredThreshold)
	{
		var sqrMagnitude = Vector3.SqrMagnitude(directionVector);

		return sqrMagnitude > squaredThreshold;
	}
	
	public static float LerpUnclamped(float a, float b, float t)
	{
		return (1f - t) * a + b * t;
	}

	public static float InverseLerpUnclamped(float a, float b, float v)
	{
		return (v - a) / (b - a);
	}
	
	public static Color RemapColor(float iMin, float iMax, Color oMin, Color oMax, float v)
	{
		return Color.Lerp(oMin, oMax, InverseLerpUnclamped(iMin, iMax, v));
	}

	public static float Remap(float iMin, float iMax, float oMin, float oMax, float v)
	{
		return Mathf.LerpUnclamped(oMin, oMax, InverseLerpUnclamped(iMin, iMax, v));
	}

	public static float RemapClamped(float iMin, float iMax, float oMin, float oMax, float v)
	{
		return Mathf.Lerp(oMin, oMax, Mathf.InverseLerp(iMin, iMax, v));
	}

	public static float ClampAngleTo (float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        return angle > 180f ? Mathf.Max(angle, 360 + @from) : Mathf.Min(angle, to);
    }

	/// <summary>
	/// Use whenever you can't find out why a canvas element is not being pressed
	/// </summary>
	public static void GetObjectUnderPointer()
	{
		//if you have InputExtensions.cs, replace these first 2 lines with appropriate simplified calls.
		if (!Input.GetMouseButtonDown(0)) return;
		
		var pointerData = new PointerEventData(EventSystem.current) {pointerId = -1, position = Input.mousePosition};

		var results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerData, results);
			
		Print(results[0].gameObject);
	}
	
	private static void Print(object msg)
	{
		Debug.Log(msg);
	}

	public static bool IsRightShoulderCloserTo(Transform attacker, Transform target)
	{
		// the hit would be approx delivered on the right shoulder if:
		// the forward of the target is in the top right or the bottom left quadrant
		// otherwise the targets left shoulder would be facing us and we would be hitting it first
		// implementation using dot products:

		var hitOnRight = false;

		var dotProduct = Vector3.Dot(attacker.forward, target.forward);
		if (dotProduct > 0.85f || dotProduct < -0.85f)
			hitOnRight = true;
		else if(Vector3.Dot(attacker.forward, target.right) < 0) 
			hitOnRight = true;

		return hitOnRight;
	}

	public static bool DoesPointLieOnTheRightSideOf(Transform subject, Vector3 point) => Vector3.Dot(subject.right, (point - subject.position).normalized) > 0;
}
