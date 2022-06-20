
using UnityEngine;

public class EnemySheildController : MonoBehaviour
{
	private EnemyRefbank _my;
	
	private bool _isSheildBroken;

	public bool IsSheildBroken => _isSheildBroken;

	private void Start()
	{
		_my = GetComponent<EnemyRefbank>();
	}


	public void OnShieldBroken()
	{
		_isSheildBroken = true;
		_my.Animations.GetHit();
	}



}
