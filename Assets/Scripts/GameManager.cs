using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    GS_PAUSEMENU,
    GS_GAME,
    GS_GAME_OVER,
    GS_LEVELCOMPLETED
}

public enum Points
{
    P_BRONZECOINS = 10,
    P_SILVERCOINS = 15,
    P_GOLDCOINS = 20,
    P_SPIKEMAN = 10
}

public class GameManager : MonoBehaviour {

    public GameState currentGameState;
    public Canvas inGameCanvas;
    public Canvas pauseMenuCanvas;
    public Canvas levelCompletedCanvas;
    public static GameManager instance;
    public Text bronzeCoinsText;
    public Text silverCoinsText;
    public Text goldCoinsText;
    public Text totalScoreText;
    public int keys = 0;
    public Image[] keysTab;
    public Image[] livesTab;

    private int bronzeCoins = 0;
    private int silverCoins = 0;
    private int goldCoins = 0;
    private int totalScore = 0;
    
    private int lives = 3;

    // Use this for initialization
    void Start () {
        bronzeCoinsText.text = bronzeCoins.ToString();
        silverCoinsText.text = silverCoins.ToString();
        goldCoinsText.text = goldCoins.ToString();
        totalScoreText.text = totalScore.ToString();

        currentGameState = GameState.GS_GAME;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentGameState.Equals(GameState.GS_PAUSEMENU) && Input.GetKeyDown(KeyCode.Escape))
        {
            InGame();
        }
        else if (currentGameState.Equals(GameState.GS_GAME) && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < keysTab.Length; i++)
            keysTab[i].color = Color.grey;
    }

    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        inGameCanvas.enabled = (newGameState == GameState.GS_GAME);
        pauseMenuCanvas.enabled = (newGameState == GameState.GS_PAUSEMENU);
        levelCompletedCanvas.enabled = (newGameState == GameState.GS_LEVELCOMPLETED);
    }

    void InGame()
    {
        SetGameState(GameState.GS_GAME);
    }

    void GameOver()
    {
        SetGameState(GameState.GS_GAME_OVER);
        PauseMenu();
    }

    void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSEMENU);
    }

    public void LevelCompleted()
    {
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    public void AddBronzeCoins()
    {
        bronzeCoins++;
        bronzeCoinsText.text = bronzeCoins.ToString();
    }

    public void AddSilverCoins()
    {
        silverCoins++;
        silverCoinsText.text = silverCoins.ToString();
    }

    public void AddGoldCoins()
    {
        goldCoins++;
        goldCoinsText.text = goldCoins.ToString();
    }

    public void IncreaseTotalScore(int number)
    {
        totalScore += number;
        totalScoreText.text = totalScore.ToString();
    }

    public void AddKeys()
    {
        keysTab[keys++].color = Color.white;
    }

    public void AddLives()
    {
        if (lives < 3)
            livesTab[lives++].color = Color.white;
    }

    public void SubLives()
    {
        if(lives > 0)
        {
            livesTab[--lives].color = Color.grey;
        }
    }

    public void OnResumeButtonClick()
    {
        InGame();
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Level2ButtonClick()
    {
        SceneManager.LoadScene("Level2");
    }
}
