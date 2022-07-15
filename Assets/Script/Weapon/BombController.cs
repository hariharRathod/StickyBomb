using System.Collections;
using System.Collections.Generic;
using FIMSpace.Jiggling;
using JellyCube;
using UnityEngine;

public class BombController : MonoBehaviour
{
	public bool IAmOnEnemy;
	public GameObject myParent;
	public RubberEffect bombRubberEffect;
	public FJiggling_Simple JigglingSimpleMesh1;

	private void Start()
	{
		if(JigglingSimpleMesh1)
			JigglingSimpleMesh1.StartJiggle(1f);
	
	}


	public void disableJiggleOnGround()
	{
		if (!JigglingSimpleMesh1) return;

		JigglingSimpleMesh1.enabled = false;
	}


	public void enableRubberEffect()
	{
		if(bombRubberEffect)
			bombRubberEffect.enabled = true;
	}

	public void disableRubberEffect()
	{
		if(bombRubberEffect)
			bombRubberEffect.enabled = false;
	}
}
