using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static GameManager instance;
    public Text bronzeCoinsText;
    public Text silverCoinsText;
    public Text goldCoinsText;
    public Text totalScoreText;
    public Image[] keysTab;
    public Image[] livesTab;

    private int bronzeCoins = 0;
    private int silverCoins = 0;
    private int goldCoins = 0;
    private int totalScore = 0;
    private int keys = 0;
    private int lives = 3;

    // Use this for initialization
    void Start () {
        bronzeCoinsText.text = bronzeCoins.ToString();
        silverCoinsText.text = silverCoins.ToString();
        goldCoinsText.text = goldCoins.ToString();
        totalScoreText.text = totalScore.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentGameState.Equals(GameState.GS_PAUSEMENU) && Input.GetKeyDown(KeyCode.S))
        {
            InGame();
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
    }

    void InGame()
    {
        SetGameState(GameState.GS_GAME);
    }

    void GameOver()
    {
        SetGameState(GameState.GS_GAME_OVER);
        PauseMenu(); //???
    }

    void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSEMENU);
    }

    void LevelCompleted()
    {
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    public void addBronzeCoins()
    {
        bronzeCoins++;
        bronzeCoinsText.text = bronzeCoins.ToString();
    }

    public void addSilverCoins()
    {
        silverCoins++;
        silverCoinsText.text = silverCoins.ToString();
    }

    public void addGoldCoins()
    {
        goldCoins++;
        goldCoinsText.text = goldCoins.ToString();
    }

    public void increaseTotalScore(int number)
    {
        totalScore += number;
        totalScoreText.text = totalScore.ToString();
    }

    public void addKeys()
    {
        keysTab[keys++].color = Color.white;
    }

    public void addLives()
    {
        if (lives < 3)
            livesTab[lives++].color = Color.white;
    }

    public void subLives()
    {
        if(lives > 0)
        {
            livesTab[--lives].color = Color.grey;
        }
    }
}
