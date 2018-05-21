using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Text level1ScoreText;
    public Text level2ScoreText;

    public static int level1Score = 0;
    public static int level2Score = 0;

    private const string text = "Your score: ";

    // Use this for initialization
    void Start () {
        level1ScoreText.text = text + level1Score.ToString();
        level2ScoreText.text = text + level2Score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        //level1ScoreText.text = text + level1Score.ToString();
        //level2ScoreText.text = text + level2Score.ToString();
	}

    IEnumerator StartGame (string levelName)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(levelName);
    }

    public void OnLevel1ButtonPressed()
    {
        StartCoroutine(StartGame("Level1"));
    }

    public void OnLevel2ButtonPressed()
    {
        StartCoroutine(StartGame("Level2"));
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
