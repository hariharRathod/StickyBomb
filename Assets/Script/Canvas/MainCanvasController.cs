using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCanvasController : MonoBehaviour	
{
	[SerializeField] private GameObject holdToAim, victory, defeat, nextLevel, retry, constantRetryButton;
	[SerializeField] private TextMeshProUGUI levelText, instructionText,releaseToShootText;
	[SerializeField] private Image red;
	[SerializeField] private string tapInstruction, swipeInstruction;
	[SerializeField] private Image dragToAimInfinityImage;
	[SerializeField] private bool showTutorial;
	
	
	[SerializeField] private Button nextLevelButton;
	private bool _hasLost;
	private bool _hasTapped;

	private void OnEnable()
	{
		GameEvents.GameLose += OnEnemyReachPlayer;
		GameEvents.GameWin += OnGameWin;
		GameEvents.FingerUp += OnFingerUp;
		GameEvents.BombRelease += OnBombRelease;

	}

	private void OnDisable()
	{
		GameEvents.GameLose -= OnEnemyReachPlayer;
		GameEvents.GameWin -= OnGameWin;
		GameEvents.FingerUp -= OnFingerUp;
		GameEvents.BombRelease -= OnBombRelease;
	}

	private void Start()
	{
		
		var levelNo = PlayerPrefs.GetInt("levelNo", 1);
		levelText.text = "Level " + levelNo;
		
		if(GA_FB.instance)
			GA_FB.instance.LevelStart(levelNo.ToString());
		
		if (!showTutorial) return;
		
		dragToAimInfinityImage.gameObject.SetActive(true);
		releaseToShootText.gameObject.SetActive(false);
		
		
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.N)) NextLevel();
		
		if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
		if(_hasTapped) return;
		
		if (!InputExtensions.GetFingerDown()) return;
		
		if(!EventSystem.current) { print("no event system"); return; }
		
		if(!EventSystem.current.IsPointerOverGameObject(InputExtensions.IsUsingTouch ? Input.GetTouch(0).fingerId : -1))
			TapToPlay();
	}

	public void NextLevel()
	{
		
		if (PlayerPrefs.GetInt("levelNo", 1) < SceneManager.sceneCountInBuildSettings - 1)
		{
			var x = PlayerPrefs.GetInt("levelNo", 1) + 1;
			PlayerPrefs.SetInt("lastBuildIndex", x);
			SceneManager.LoadScene(x);
		}
		else
		{
			var x = Random.Range(0, SceneManager.sceneCountInBuildSettings - 1);
			PlayerPrefs.SetInt("lastBuildIndex", x);
			SceneManager.LoadScene(x);
		}
		PlayerPrefs.SetInt("levelNo", PlayerPrefs.GetInt("levelNo", 1) + 1);
		
		AudioManager.instance.Play("ButtonPress");

	}


	private void TapToPlay()
	{
		_hasTapped = true;
		holdToAim.SetActive(false);
		GameEvents.InvokeOnTapToPlay();
		if (!showTutorial) return;
		dragToAimInfinityImage.gameObject.SetActive(false);
		releaseToShootText.gameObject.SetActive(true);
	
	}
	private void EnableVictoryObjects()
	{
		if(defeat.activeSelf) return;
		
		victory.SetActive(true);
		nextLevelButton.gameObject.SetActive(true);
		nextLevelButton.interactable = true;
		constantRetryButton.SetActive(false);
		
		AudioManager.instance.Play("Win");
		
	}
	
	private void EnableLossObjects()
	{
		if(victory.activeSelf) return;

		if (_hasLost) return;
		
		red.enabled = true;
		var originalColor = red.color;
		red.color = Color.clear;
		red.DOColor(originalColor, 1f);

		defeat.SetActive(true);
		retry.SetActive(true);
		//skipLevel.SetActive(true);
		constantRetryButton.SetActive(false);
		_hasLost = true;
		
		AudioManager.instance.Play("Lose");
		
		
	}
	
	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		AudioManager.instance.Play("ButtonPress");
	}
	
	private void OnEnemyReachPlayer()
	{
		Invoke(nameof(EnableLossObjects), 1.5f);
		var levelNo = PlayerPrefs.GetInt("levelNo", 1);
		if(GA_FB.instance)
			GA_FB.instance.LevelFail(levelNo.ToString());
	
	}

	private void OnGameWin()
	{
		Invoke(nameof(EnableVictoryObjects), 1f);
		var levelNo = PlayerPrefs.GetInt("levelNo", 1);
		if(GA_FB.instance)
			GA_FB.instance.LevelCompleted(levelNo.ToString());
	
	}
	
	private void OnFingerUp()
	{
		if (!showTutorial) return;
		
		releaseToShootText.gameObject.SetActive(false);
	}
	
	private void OnBombRelease()
	{
		if (!showTutorial) return;
		
		releaseToShootText.gameObject.SetActive(false);
	}
}
