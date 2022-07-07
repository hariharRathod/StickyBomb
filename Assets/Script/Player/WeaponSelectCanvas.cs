using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectCanvas : MonoBehaviour
{
	[SerializeField] private bool showTutorial;
	[SerializeField] private Button weaponSelectButton;
	[SerializeField] private Image bombImage, arrowImage,arrowPointDown;
	[SerializeField] private Button bombButton, arrowButton;
	[SerializeField] private bool enableTwoButtonsForVideo;
	[SerializeField] private Image bombBackgroundImage, arrowBackgroundImage;
	[SerializeField] private Color enableColor,disableColor;
	
	private PlayerRefBank _my;
	private Tween arrowPointDownTween;

	private void OnEnable()
	{
		WeaponEvents.OnArrowSelectEvent += OnArrowWeaponSelected;
		WeaponEvents.OnBombSelectEvent += OnBombWeaponSelected;
		GameEvents.GameLose += DisableWeaponSelectButton;
		GameEvents.GameWin += DisableWeaponSelectButton;
		GameEvents.CameraFollowArrowStart += DisableWeaponSelectButton;
		GameEvents.BombRelease += OnBombRelease;
	}

	private void OnDisable()
	{
		WeaponEvents.OnArrowSelectEvent -= OnArrowWeaponSelected;
		WeaponEvents.OnBombSelectEvent -= OnBombWeaponSelected;
		GameEvents.GameLose -= DisableWeaponSelectButton;
		GameEvents.GameWin -= DisableWeaponSelectButton;
		GameEvents.CameraFollowArrowStart -= DisableWeaponSelectButton;
		GameEvents.BombRelease -= OnBombRelease;
	}

	private void Start()
	{
		_my = GetComponent<PlayerRefBank>();

		if (!showTutorial) return;
		
		arrowPointDown.gameObject.SetActive(false);
	}

	//ye phele ek button se chalen wla code hai.
	/*private void OnArrowWeaponSelected()
	{
		DisableArrowPointDown();
		bombImage.rectTransform.localScale=Vector3.zero;
		DisableWeaponSelectButton();
		arrowImage.rectTransform.DOScale(Vector3.zero,0.25f).SetEase(Ease.InBack).OnComplete(
			() =>
			{
				bombImage.rectTransform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InBack).OnComplete(EnableWeaponSelectButton);
			});
		
		
	}
	
	private void OnBombWeaponSelected()
	{
		arrowImage.rectTransform.localScale = Vector3.zero;
		DisableWeaponSelectButton();
		bombImage.rectTransform.DOScale(Vector3.zero,0.25f).SetEase(Ease.InBack).OnComplete(
			() =>
			{
				arrowImage.rectTransform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InBack).OnComplete(EnableWeaponSelectButton);
			});
	}*/
	
	//ye two button se chalne wala code.
	private void OnArrowWeaponSelected()
	{
		
		arrowButton.interactable = true;
		arrowBackgroundImage.color = enableColor;
		bombBackgroundImage.color = disableColor;
	
	}
	
	private void OnBombWeaponSelected()
	{
		
		bombButton.interactable = true;
		arrowBackgroundImage.color = disableColor;
		bombBackgroundImage.color = enableColor;
	}
	
	public void OnWeaponSelect()
	{
		//bhai time mila to dekho kya scene hai ishar,_my.weaponslect kiya to null bolra hai ye.
		//_my.WeaponSelect.WeaponChange();
		//ye event call singlecast jaisa work karra hai kuch kar iska.
		WeaponEvents.InvokeWeaponActivate();
		AudioManager.instance.Play("WeaponButton");
	}

	public void OnBombButtonPressed()
	{
		WeaponEvents.InvokeOnBombButtonPressed();
	}
	public void OnArrowButtonPressed()
	{
		WeaponEvents.InvokeOnArrowButtonPressed();
	}

	public void EnableWeaponSelectButton()
	{
		weaponSelectButton.interactable = true;
	}

	public void DisableWeaponSelectButton()
	{
		weaponSelectButton.interactable = false;
	}
	
	private void OnBombRelease()
	{
		if (!showTutorial) return;
		
		arrowPointDown.gameObject.SetActive(true);
		arrowPointDownTween= arrowPointDown.rectTransform.DOLocalMoveY(arrowPointDown.rectTransform.localPosition.y + 40f, 0.5f)
			.SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

	}

	private void DisableArrowPointDown()
	{
		if (!showTutorial) return;
		
		arrowPointDownTween.Kill();
		arrowPointDown.gameObject.SetActive(false);
	}

}
