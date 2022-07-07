using System.Collections;
using System.Collections.Generic;
using JellyCube;
using UnityEngine;

public class BombController : MonoBehaviour
{
	public bool IAmOnEnemy;
	public GameObject myParent;
	public RubberEffect bombRubberEffect;

	public void enableRubberEffect()
	{
		bombRubberEffect.enabled = true;
	}

	public void disableRubberEffect()
	{
		bombRubberEffect.enabled = false;
	}
}
