
using UnityEngine;

public class LevelFlowController : MonoBehaviour
{
	public static LevelFlowController only;
	public int currentArea;
	[SerializeField] public float maxRayDistance = 50f;
	
	private void Awake()
	{
		if (!only) only = this;
		else Destroy(gameObject);
	}
}
