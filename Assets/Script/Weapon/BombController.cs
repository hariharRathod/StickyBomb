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
	public FJiggling_Simple jiggle;

	private void Start()
	{
		jiggle.StartJiggle(1f);
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
