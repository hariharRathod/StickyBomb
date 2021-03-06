using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AimController : MonoBehaviour
{
	//the magic number percentage 0.5905f is the screen Y pos when you center the crosshair on anchorY as minY = 0.565, maxY = 0.615
	//0.5899 for 0.55, 0.63
	[Header("Aiming")] public float screenPercentageOnY = 0.5899f, aimLerp;
	
	[SerializeField] private float aimSpeedVertical, aimSpeedHorizontal, clampAngleVertical, clampAngleHorizontal;
	[SerializeField] private Color findTargetColor, missingTargetColor;
	private Image _reticle;
	
	private Transform _transform;
	
	private float _rotX, _rotY, _desiredRotX, _desiredRotY, _initRotAxisX, _initRotAxisY;
	private float _targetDistance, _targetInitYPos, _targetDesiredYPos;
	private Quaternion _areaInitRotation;
	private Canvas _canvas;

	private void OnEnable()
	{
		GameEvents.ReactNextArea += OnReachNextArea;
		GameEvents.GameLose += OnGameLose;
		GameEvents.GameWin += OnGameWin;
	}

	private void OnDisable()
	{
		GameEvents.ReactNextArea -= OnReachNextArea;
		GameEvents.GameLose -= OnGameLose;
		GameEvents.GameWin -= OnGameWin;
	}

	private void Start()
	{
		_transform = transform;
		_canvas = GameObject.FindGameObjectWithTag("AimCanvas").GetComponent<Canvas>();
		
		_canvas.worldCamera = Camera.main;
		_canvas.planeDistance = 1f;
		_reticle = _canvas.transform.GetChild(0).GetComponent<Image>();
		
		_areaInitRotation = _transform.rotation;
		var rot = _areaInitRotation.eulerAngles;

		_initRotAxisX = rot.x;
		_initRotAxisY = rot.y;
		
		_rotY = rot.y;
		_rotX = rot.x;
	}


	public void Aim(Vector2 inputDelta)
	{
		_rotY += inputDelta.x * aimSpeedHorizontal * Time.deltaTime;
		_rotX -= inputDelta.y * aimSpeedVertical * Time.deltaTime;
 
		_rotY = Mathf.Clamp(_rotY, _initRotAxisY - clampAngleHorizontal, _initRotAxisY + clampAngleHorizontal);
		_rotX = Mathf.Clamp(_rotX, _initRotAxisX - clampAngleVertical, _initRotAxisX + clampAngleVertical);

		/*_desiredRotY += inputDelta.x * aimSpeedHorizontal * Time.deltaTime;
		_desiredRotX -= inputDelta.y * aimSpeedVertical * Time.deltaTime;
 
		_desiredRotY = Mathf.Clamp(_rotY, _initRotAxisY - clampAngleHorizontal, _initRotAxisY + clampAngleHorizontal);
		_desiredRotX = Mathf.Clamp(_rotX, _initRotAxisX - clampAngleVertical, _initRotAxisX + clampAngleVertical);

		_rotX = Mathf.Lerp(_rotX, _desiredRotX, Time.deltaTime * aimLerp);
		_rotY = Mathf.Lerp(_rotY, _desiredRotY, Time.deltaTime * aimLerp);
		*/

		var newRot = Quaternion.Euler(_rotX, _rotY, 0.0f);
		_transform.rotation = newRot;
	}
	
	public void SetReticleStatus(bool isOn)
	{
		_reticle.enabled = isOn;
	}
	
	public void LoseTarget()
	{
		_reticle.color = missingTargetColor;
		
	}
	
	public void FindTarget()
	{
		_reticle.color = findTargetColor;
		
	}

	public void ResetRotation()
	{
		_transform.DORotateQuaternion(_areaInitRotation, 0.2f).SetDelay(0.2f);
		_rotX = _initRotAxisX;
		_rotY = _initRotAxisY;
	}
	
	private void OnReachNextArea()
	{
		_areaInitRotation = _transform.rotation;
		var rot = _areaInitRotation.eulerAngles;

		_initRotAxisX = rot.x;
		_initRotAxisY = rot.y;
		
		_rotY = rot.y;
		_rotX = rot.x;
	}
	
	private void OnGameWin()
	{
		SetReticleStatus(false);
	}

	private void OnGameLose()
	{
		SetReticleStatus(false);
	}
}
