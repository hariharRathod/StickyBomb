using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class ArrowFollowCamera : MonoBehaviour
{
	private GameObject _arrowToFollow;

	private static Transform EnemyTarget;

	[SerializeField] private float YcameraRotateAngle;
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

		
		transform.localPosition =new Vector3(-1, 0, -1.3f);
		
		transform.localEulerAngles = new Vector3(0,57f,0);
		//
		//transform.GetComponent<Camera>().fieldOfView.To(60, 0.2f);
		
	}
	
	private void OnArrowCollisionWithTarget()
	{
		transform.parent = null;
		
		//if (!EnemyTarget) return;

		transform.DORotateQuaternion(Quaternion.Euler(transform.eulerAngles.x, YcameraRotateAngle, 0), 0.8f);

		//Tween _cameraLookTween = DOVirtual.DelayedCall(0.1f,SetRotation).SetLoops(-1);

		//DOVirtual.DelayedCall(6f, () => _cameraLookTween.Kill());
	}

	private void SetRotation()
	{
		
		var dir = EnemyTarget.position - transform.position;
		//transform.rotation = Quaternion.LookRotation(dir,Vector3.up);
		transform.rotation =Quaternion.Euler(transform.eulerAngles.x,dir.y,0);
		
		
	}

	public static void OnLastEnemyStanding(Transform obj)
	{
		EnemyTarget = obj;
	}
}