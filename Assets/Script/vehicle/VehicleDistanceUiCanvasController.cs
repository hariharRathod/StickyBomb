using UnityEngine;
using UnityEngine.UI;

public class VehicleDistanceUiCanvasController : MonoBehaviour
{
	public static VehicleDistanceUiCanvasController only;
	
	[SerializeField] private Image distanceBar;
	
	private Canvas _canvas;

	private void OnEnable()
	{
		GameEvents.GameWin += OnGameEnd;
		GameEvents.GameLose += OnGameEnd;
	}

	private void OnDisable()
	{
		GameEvents.GameWin -= OnGameEnd;
		GameEvents.GameLose -= OnGameEnd;
	}

	private void Awake()
	{
		if (!only) only = this;
		else Destroy(only);
	}

	private void Start()
	{
		_canvas = GetComponent<Canvas>();
	}

	private void DisableCanvas() => _canvas.enabled = false;

	public void SetDistanceUI(float normalizedValue) => distanceBar.fillAmount = normalizedValue;

	private void OnGameEnd()
	{
		DisableCanvas();
	}
}
