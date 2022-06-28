
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectCanvas : MonoBehaviour
{
	[SerializeField] private bool showTutorial;
	[SerializeField] private Button weaponSelectButton;
	[SerializeField] private Image bombImage, arrowImage,arrowPointDown;
	
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

	
	private void OnArrowWeaponSelected()
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
	}

	public void OnWeaponSelect()
	{
		//bhai time mila to dekho kya scene hai ishar,_my.weaponslect kiya to null bolra hai ye.
		//_my.WeaponSelect.WeaponChange();
		//ye event call singlecast jaisa work karra hai kuch kar iska.
		WeaponEvents.InvokeWeaponActivate();
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
