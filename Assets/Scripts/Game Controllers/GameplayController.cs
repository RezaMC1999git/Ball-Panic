using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
	public static GameplayController instance;

	[SerializeField] private GameObject[] topAndBottomBricks, leftBricks, rightBricks;
	[SerializeField] private GameObject[] players;
	[SerializeField] private GameObject[] endOfLevelRewards;
	[SerializeField] private Button pauseBtn;

	public GameObject panelBG, levelFinishedPanel, playerDiedPanel, pausePanel;
	private GameObject topBrick, bottomBrick, leftBrick, rightBrick;
	private Vector3 coordinates;
	public float levelTime;
	public Text liveText, scoreText, levelTimerText, showScoreAtTheEndOfLevelText, countDownAndBeginLevelText, watchVideoText;
	private float countDownBeforeLevelBegins = 3.0f;
	public static int smallBallsCount = 0;
	public int playerLives, playerScore, coins;
	private bool isGamePaused, hasLevelBegan, countdownLevel;
	public bool levelInProgress;

	private void Awake()
	{
		CreateInstance();
		InitializeBricksAndPlayer();
	}
    private void Start()
    {
		InitializeGameplayController();
	}
    private void Update()
    {
		UpdateGameplayController();
		if (Input.GetKeyDown(KeyCode.K))
        {
			smallBallsCount = 1;
			CountSmallBalls();
		}
    }
    void CreateInstance()
	{
		if (instance == null)
			instance = this;
	}
	void InitializeBricksAndPlayer()
	{

		coordinates = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		int index = Random.Range(0, topAndBottomBricks.Length);

		topBrick = Instantiate(topAndBottomBricks[index]);
		bottomBrick = Instantiate(topAndBottomBricks[index]);
		leftBrick = Instantiate(leftBricks[index], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, -90))) as GameObject;
		rightBrick = Instantiate(rightBricks[index], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;

		topBrick.tag = "TopBrick";

		topBrick.transform.position = new Vector3(-coordinates.x - 4.81f, coordinates.y - 2.19f, 0);
		bottomBrick.transform.position = new Vector3(-coordinates.x - 5.7f, -coordinates.y - 2.29f, 0);
		leftBrick.transform.position = new Vector3(-coordinates.x - 2.37f, coordinates.y + 8.51f, 0);
		rightBrick.transform.position = new Vector3(coordinates.x + 2.32f, -coordinates.y - 10.75f, 0);

		Instantiate(players[GameController.instance.selectedPlayer]);

	}
	void InitializeGameplayController()
	{

		if (GameController.instance.isGameStartedFromLevelMenu)
		{
			playerScore = 0;
			playerLives = 2;
			GameController.instance.currentScore = playerScore;
			GameController.instance.currentLives = playerLives;
			GameController.instance.isGameStartedFromLevelMenu = false;
		}
		else
		{
			playerScore = GameController.instance.currentScore;
			playerLives = GameController.instance.currentLives;
		}

		levelTimerText.text = levelTime.ToString("F0");
		scoreText.text = "Score x" + playerScore;
		liveText.text = "x" + playerLives;

		Time.timeScale = 0;
		countDownAndBeginLevelText.text = countDownBeforeLevelBegins.ToString("F0");
	}
	void CountdownAndBeginLevel()
	{
		countDownBeforeLevelBegins -= (0.05f * 0.15f);
		countDownAndBeginLevelText.text = countDownBeforeLevelBegins.ToString("F0");
		if (countDownBeforeLevelBegins <= 0)
		{
			Time.timeScale = 1f;
			hasLevelBegan = false;
			levelInProgress = true;
			countdownLevel = true;
			countDownAndBeginLevelText.gameObject.SetActive(false);
		}
	}

	public void setHasLevelBegan(bool hasLevelBegan)
	{
		this.hasLevelBegan = hasLevelBegan;
	}
	void UpdateGameplayController()
	{
		scoreText.text = "Score x" + playerScore;
		if (hasLevelBegan)
		{
			CountdownAndBeginLevel();
		}
		if (countdownLevel)
		{
			LevelCountdownTimer();
		}
	}
	void LevelCountdownTimer()
	{

		if (Time.timeScale == 1)
		{

			levelTime -= Time.deltaTime;
			levelTimerText.text = levelTime.ToString("F0");

			if (levelTime <= 0)
			{

				playerLives--;
				GameController.instance.currentLives = playerLives;
				GameController.instance.currentScore = playerScore;

				if (playerLives < 0)
				{
					StartCoroutine(PromptTheUserToWatchAVideo());
				}
				else
				{
					StartCoroutine(PlayerDiedRestartLevel());
				}

			}

		}

	}
	public void GoToMapButton()
	{

		GameController.instance.currentScore = playerScore;

		if (GameController.instance.highScore < GameController.instance.currentScore)
		{
			GameController.instance.highScore = GameController.instance.currentScore;
			GameController.instance.Save();
		}

		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
		}

		Application.LoadLevel("LevelMenu");

		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.PlayLoadingScreen();
		}

	}
	public void RestartCurrentLevelButton()
	{

		smallBallsCount = 0;
		coins = 0;

		GameController.instance.currentLives = playerLives;
		GameController.instance.currentScore = playerScore;

		Application.LoadLevel(Application.loadedLevelName);

		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.PlayLoadingScreen();
		}

	}
	public void NextLevel()
	{
		GameController.instance.currentScore = playerScore;
		GameController.instance.currentLives = playerLives;

		if (GameController.instance.highScore < GameController.instance.currentScore)
		{
			GameController.instance.highScore = GameController.instance.currentScore;
			GameController.instance.Save();
		}

		int nextLevel = GameController.instance.currentLevel;
        if (nextLevel < 3)
        {
			if (!(nextLevel >= GameController.instance.levels.Length))
			{
				Application.LoadLevel("Level " + nextLevel);

				if (LoadingScreen.instance != null)
				{
					LoadingScreen.instance.PlayLoadingScreen();
				}
			}
		}
		else
        {
			Application.LoadLevel("LevelMenu");

			if (LoadingScreen.instance != null)
			{
				LoadingScreen.instance.PlayLoadingScreen();
			}
		}
	}
	public void PauseGame()
	{
		if (!hasLevelBegan)
		{
			if (levelInProgress)
			{
				if (!isGamePaused)
				{
					countdownLevel = false;
					levelInProgress = false;
					isGamePaused = true;

					panelBG.SetActive(true);
					pausePanel.SetActive(true);

					Time.timeScale = 0;
				}
			}
		}
	}
	public void ResumeGame()
	{

		countdownLevel = true;
		levelInProgress = true;
		isGamePaused = false;
		panelBG.SetActive(false);
		pausePanel.SetActive(false);

		Time.timeScale = 1;

	}
	public void DontWatchVideoInsteadQuit()
	{
		GameController.instance.currentScore = playerScore;
		if (GameController.instance.highScore < GameController.instance.currentScore)
		{
			GameController.instance.highScore = GameController.instance.currentScore;
			GameController.instance.Save();
		}

		Time.timeScale = 1;
		Application.LoadLevel("LevelMenu");
		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.PlayLoadingScreen();
		}

	}
	IEnumerator GivePlayerLivesRewardAfterWatchingVideo()
	{

		watchVideoText.text = "Thanks For Watching !";

		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(2f));

		coins = 0;
		playerLives = 2;
		smallBallsCount = 0;

		GameController.instance.currentLives = playerLives;
		GameController.instance.currentScore = playerScore;

		Time.timeScale = 0;

		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.FadeOut();
		}

		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1.25f));

		Application.LoadLevel(Application.loadedLevelName);

		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.PlayFadeInAnimation();
		}

	}
	public void PlayerDied()
	{

		countdownLevel = false;
		pauseBtn.interactable = false;
		levelInProgress = false;

		smallBallsCount = 0;

		playerLives--;
		GameController.instance.currentLives = playerLives;
		GameController.instance.currentScore = playerScore;

		if (playerLives < 0)
		{
			StartCoroutine(PromptTheUserToWatchAVideo());
		}
		else
		{
			StartCoroutine(PlayerDiedRestartLevel());
		}

	}
	IEnumerator PlayerDiedRestartLevel()
	{
		levelInProgress = false;
		coins = 0;
		smallBallsCount = 0;
		Time.timeScale = 0;
		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.FadeOut();
		}
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1.25f));

		Application.LoadLevel(Application.loadedLevelName);

		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.PlayFadeInAnimation();
		}

	}
	IEnumerator PromptTheUserToWatchAVideo()
	{
		levelInProgress = false;
		countdownLevel = false;
		pauseBtn.interactable = false;
		Time.timeScale = 0;
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(.7f));
		playerDiedPanel.SetActive(true);
	}

	IEnumerator LevelCompleted() {
		Debug.Log(GameController.instance.currentLevel);
		countdownLevel = false;
		pauseBtn.interactable = false;
		int unlockedLevel = GameController.instance.currentLevel;
		if(GameController.instance.currentLevel == -1)
			unlockedLevel++;
		unlockedLevel++;
		if (!(unlockedLevel >= GameController.instance.levels.Length) && unlockedLevel <3) {
			GameController.instance.levels[unlockedLevel] = true;
			GameController.instance.currentLevel = unlockedLevel;
		}
		Instantiate (endOfLevelRewards[GameController.instance.currentLevel+1],
		             new Vector3(0, -1.5f, 0), Quaternion.identity);
		if (GameController.instance.doubleCoins) {
			coins *= 2;
		}
		GameController.instance.coins = GameController.instance.coins +  coins;
		GameController.instance.Save();
		yield return StartCoroutine (MyCoroutine.WaitForRealSeconds(4f));
		levelInProgress = false;
		PlayerScript.instance.StopMoving ();
		Time.timeScale = 0;
		levelFinishedPanel.SetActive (true);
		showScoreAtTheEndOfLevelText.text = "" + playerScore;
	}
	public void CountSmallBalls()
	{
		smallBallsCount--;

		if (smallBallsCount == 0)
		{
			StartCoroutine(LevelCompleted());
		}
	}
}
