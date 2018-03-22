using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

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
