using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    public Text timerText;
    public float timerSpeed = 1f;
    public static float timer = 0;

    private float seconds = 0;
    private float minutes = 0;
    
    // Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.currentGameState == GameState.GS_GAME)
        {
            timer += (Time.deltaTime * timerSpeed);
            minutes = (int)(timer / 60);
            seconds = (int)(timer % 60);

            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
	}
}
