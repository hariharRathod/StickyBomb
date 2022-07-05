
using DG.Tweening;
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
		print("_arrowToFollow: " + _arrowToFollow.name);

		
		transform.localPosition =new Vector3(-1.6f, 0, -1.8f);
		
		transform.localEulerAngles = new Vector3(0,57f,0);
		//
		//transform.GetComponent<Camera>().fieldOfView.To(60, 0.2f);
	}
	
	private void OnArrowCollisionWithTarget()
	{
		transform.parent = null;
	}
}