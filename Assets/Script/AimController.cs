using UnityEngine;
using UnityEngine.UI;

public class AimController : MonoBehaviour
{
	//the magic number percentage 0.5905f is the screen Y pos when you center the crosshair on anchorY as minY = 0.565, maxY = 0.615
	//0.5899 for 0.55, 0.63
	[Header("Aiming")] public float screenPercentageOnY = 0.5899f;
	
	[SerializeField] private float aimSpeedVertical, aimSpeedHorizontal, clampAngleVertical, clampAngleHorizontal;
	[SerializeField] private Color findTargetColor, missingTargetColor;
	private Image _reticle;
	
	private Transform _transform;
	
	private float _rotX, _rotY, _initRotAxisX, _initRotAxisY;
	private float _targetDistance, _targetInitYPos, _targetDesiredYPos;
	private Quaternion _areaInitRotation;
	private Canvas _canvas;


	private void Start()
	{
		_canvas = GameObject.FindGameObjectWithTag("AimCanvas").GetComponent<Canvas>();
		
		_canvas.worldCamera = Camera.main;
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

		var newRot = Quaternion.Euler(_rotX, _rotY, 0.0f);
		_transform.rotation = newRot;
	}
}
