
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCanvasController : MonoBehaviour
{
	[SerializeField] private GameObject holdToAim, victory, defeat, nextLevel, retry, constantRetryButton;
	[SerializeField] private TextMeshProUGUI levelText, instructionText;
	[SerializeField] private Image red;
	[SerializeField] private string tapInstruction, swipeInstruction;
	
	[SerializeField] private Button nextLevelButton;
	private bool _hasLost;
	private bool _hasTapped;

	private void OnEnable()
	{
		GameEvents.GameLose += OnEnemyReachPlayer;
		GameEvents.GameWin += OnGameWin;
	}

	private void OnDisable()
	{
		GameEvents.GameLose -= OnEnemyReachPlayer;
		GameEvents.GameWin -= OnGameWin;
	}

	private void Start()
	{
		//var levelNo = PlayerPrefs.GetInt("levelNo", 1);
		var levelNo = SceneManager.GetActiveScene().buildIndex + 1;
		levelText.text = "Level " + levelNo;
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
		/*if (PlayerPrefs.GetInt("levelNo", 1) < SceneManager.sceneCountInBuildSettings - 1)
		{
			var x = PlayerPrefs.GetInt("levelNo", 1) + 1;
			PlayerPrefs.SetInt("lastBuildIndex", x);
			SceneManager.LoadScene(x);
		}
		else
		{
			var x = Random.Range(0, SceneManager.sceneCountInBuildSettings - 1);
			if (PlayerPrefs.GetInt("levelNo", 1) % 10 == 0)
				x = 12;
			PlayerPrefs.SetInt("lastBuildIndex", x);
			SceneManager.LoadScene(x);
		}
		PlayerPrefs.SetInt("levelNo", PlayerPrefs.GetInt("levelNo", 1) + 1);*/


		var levelNo = SceneManager.GetActiveScene().buildIndex;
		if (levelNo < SceneManager.sceneCountInBuildSettings-1)
		{
			levelNo += 1;
			SceneManager.LoadScene(levelNo);
		}
		else
		{
			var x = Random.Range(0, SceneManager.sceneCountInBuildSettings);
			SceneManager.LoadScene(x);

		}


	}


	private void TapToPlay()
	{
		_hasTapped = true;
		holdToAim.SetActive(false);
		GameEvents.InvokeOnTapToPlay();
	
	}
	private void EnableVictoryObjects()
	{
		if(defeat.activeSelf) return;
		
		victory.SetActive(true);
		nextLevelButton.gameObject.SetActive(true);
		nextLevelButton.interactable = true;
		constantRetryButton.SetActive(false);
		
		
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
		
		
	}
	
	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
	}
	
	private void OnEnemyReachPlayer()
	{
		Invoke(nameof(EnableLossObjects), 1.5f);
		
		
	}

	private void OnGameWin()
	{
		Invoke(nameof(EnableVictoryObjects), 1f);
		
		
	}
}
