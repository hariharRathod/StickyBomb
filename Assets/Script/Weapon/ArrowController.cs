using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowController : MonoBehaviour
{
	public void DestroyArrows(List<GameObject> arrowList)
	{
		if (!arrowList.Contains(this.gameObject)) return;
		//DOVirtual.DelayedCall(3f, () => Destroy(this.gameObject));
	}

}
