
using UnityEngine;

public class ArrowFollowCamera : MonoBehaviour
{
	private GameObject _arrowToFollow;
	private void OnEnable()
	{
		WeaponEvents.ArrowCollisonWithTargetDone += OnArrowCollisionWithTarget;
	}

	private void OnDisable()
	{
		WeaponEvents.ArrowCollisonWithTargetDone -= OnArrowCollisionWithTarget;
	}

	public void OnCameraFollowArrow(GameObject arrow)
	{
		gameObject.SetActive(true);
		_arrowToFollow = arrow;
		if (!_arrowToFollow)
		{
			print("Bro arrow Mila nahi issee follow karne ko");
			return;
		}

		transform.parent = _arrowToFollow.transform;
		transform.localPosition =new Vector3(-2f, 0, -2f);
		transform.localEulerAngles = new Vector3(0,50f,0);
	}
	
	private void OnArrowCollisionWithTarget()
	{
		transform.parent = null;
	}
}