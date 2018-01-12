﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Canvas menuCanvas;

	public Text scoreText;
	public string prefix = "Yellow Thingys: ";
	public string winText = "You Win!!!! Score!!: ";
	public string winMaybeText = "You Win??? Score??: ";
	public int winScore = 4;
	public int score = 0;
	public PlayerController player;
	public Text timerText;
	public float timer = 0f;

	private bool won = false;

	// Use this for initialization
	void Start () {
		
	}

	public void AddScore (int add) {
		score += add;
		won = false;
		scoreText.text = prefix + score;
	}

	public void Win (string nextLevel) {
		won = true;
		bool reallyWon = score >= winScore;
		if (reallyWon) {
			SceneManager.LoadScene(nextLevel);
		}
		scoreText.text = winMaybeText + score * 50;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.hasMoved) {
			timer += Time.deltaTime;
			// Thanks https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html
			timerText.text = string.Format("{0:0}:{1:00}.{2:00}",
                     		 Mathf.Floor(timer / 60), // minutes
                    		 Mathf.Floor(timer) % 60, // seconds
                    		 Mathf.Floor((timer * 100) % 100)); // miliseconds
		}
	}

	public void TogglePauseMenu() {
		if (menuCanvas.enabled) {
			menuCanvas.enabled = false;
			Time.timeScale = 1.0f;
		} else {
			menuCanvas.enabled = true;
			Time.timeScale = 0f;
		}
	}

	public void Quit () {
		Application.Quit();
	}

	public void LoadLevel (string level) {
		SceneManager.LoadScene(level);
		menuCanvas.enabled = false;
		Time.timeScale = 1.0f;
	}
}
