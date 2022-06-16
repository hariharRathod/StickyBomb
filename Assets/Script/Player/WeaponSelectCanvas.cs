
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectCanvas : MonoBehaviour
{
	[SerializeField] private Button weaponSelectButton;
	[SerializeField] private Image bombImage, arrowImage;
	
	private PlayerRefBank _my;

	private void OnEnable()
	{
		WeaponEvents.OnArrowSelectEvent += OnArrowWeaponSelected;
		WeaponEvents.OnBombSelectEvent += OnBombWeaponSelected;
		GameEvents.GameLose += DisableWeaponSelectButton;
		GameEvents.GameWin += DisableWeaponSelectButton;
		GameEvents.CameraFollowArrowStart += DisableWeaponSelectButton;
	}

	private void OnDisable()
	{
		WeaponEvents.OnArrowSelectEvent -= OnArrowWeaponSelected;
		WeaponEvents.OnBombSelectEvent -= OnBombWeaponSelected;
		GameEvents.GameLose -= DisableWeaponSelectButton;
		GameEvents.GameWin -= DisableWeaponSelectButton;
		GameEvents.CameraFollowArrowStart -= DisableWeaponSelectButton;
	}

	private void Start()
	{
		_my = GetComponent<PlayerRefBank>();
	}

	
	private void OnArrowWeaponSelected()
	{
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

}
