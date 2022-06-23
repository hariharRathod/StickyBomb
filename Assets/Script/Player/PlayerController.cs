
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private PlayerRefBank _my;
	[SerializeField] private ArrowFollowCamera _arrowFollowCamera;
	private GameObject _arrowToFollow;

	private void Start()
	{
		_my = GetComponent<PlayerRefBank>();
		
		
	}


	private void OnEnable()
	{
		//WeaponEvents.ArrowRealeaseEvent += GetArrowFromArrowReleaseEvent;
		GameEvents.CameraFollowArrowStart += OnCameraFollowArrowStart;
	}

	private void OnDisable()
	{
		//WeaponEvents.ArrowRealeaseEvent -= GetArrowFromArrowReleaseEvent;
		GameEvents.CameraFollowArrowStart -= OnCameraFollowArrowStart;
	}


	private void GetArrowFromArrowReleaseEvent(Transform target,GameObject arrow)
	{
		_arrowToFollow = arrow;
		_arrowToFollow.gameObject.name = "ArrowCameraFollow";
	}
	
	public void SetArrowToFollow(GameObject arrow)
	{
		_arrowToFollow = arrow;
		_arrowToFollow.gameObject.name = "ArrowCameraFollow";
	}

	private void OnCameraFollowArrowStart()
	{
		_my.Camera.gameObject.SetActive(false);
		_arrowFollowCamera.OnCameraFollowArrow(_arrowToFollow);
	}
}
