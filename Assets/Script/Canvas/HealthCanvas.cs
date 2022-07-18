using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
	[SerializeField] private Image healthBar;

	private Canvas _canvas;

	private void Start()
	{
		_canvas = GetComponent<Canvas>();
	}

	public void DisableCanvas()
	{
		if (!_canvas) return;
		
		_canvas.enabled = false;
	} 
	
	public void SetHealth(float normalizedHealthValue) => DOTween.To(
		() => healthBar.fillAmount, value => healthBar.fillAmount = value, normalizedHealthValue, 0.25f);
}
