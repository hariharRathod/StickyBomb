using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController only; 
	
	[SerializeField] private float actionFov, zoomDuration;

	[Header("ScreenShake"), SerializeField]
	private float shakeDuration;
	[SerializeField] private float shakeStrength;
	
	private Vector3 _initialLocalPos;
	private float _normalFov;
	private Camera _me;
	private bool onceShakeDone;
	
	private void OnEnable()
	{
		WeaponEvents.ArrowRealeaseEvent += OnArrowRelase;
		WeaponEvents.OnMultipleArrowCollison += OnMultipleArrowCollison;
		WeaponEvents.OnBombExplosion += OnBombExplosion;
	}

	private void OnDisable()
	{
		WeaponEvents.ArrowRealeaseEvent -= OnArrowRelase;
		WeaponEvents.OnMultipleArrowCollison -= OnMultipleArrowCollison;
		WeaponEvents.OnBombExplosion -= OnBombExplosion;
	}

	private void Awake()
	{
		if (!only) only = this;
		else Destroy(only);
	}

	private void Start()
	{
		_me = GetComponent<Camera>();
		_me.depthTextureMode |= DepthTextureMode.Depth;

		_normalFov = _me.fieldOfView;
		_initialLocalPos = transform.localPosition;
	}
	
	private void ZoomNormal()
	{
		DOTween.To(() => _me.fieldOfView, value => _me.fieldOfView = value, _normalFov, zoomDuration);
	}

	private void ZoomAction()
	{
		DOTween.To(() => _me.fieldOfView, value => _me.fieldOfView = value, actionFov, zoomDuration).OnComplete(ZoomNormal);
	}

	public void ScreenShake(float intensity)
	{
		_me.DOShakePosition(shakeDuration * intensity / 2f, shakeStrength * intensity, 10, 45f).OnComplete(() =>
		{
			
			transform.DOLocalMove(_initialLocalPos, 0.15f);
		});
	}
	
	private void OnArrowRelase(Transform arg1, GameObject arg2)
	{
		onceShakeDone = false;
	}
	
	private void OnBombExplosion()
	{
		ScreenShake(2f);
	}
	
	private void OnMultipleArrowCollison()
	{
		if (onceShakeDone) return;
		onceShakeDone = true;
		ScreenShake(2f);
	}
	
	private void OnEnemyReachPlayer()
	{
		ZoomAction();
	}
}