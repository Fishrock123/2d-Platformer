using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Canvas menuCanvas;

	public Text textComponent;
	public string prefix = "Yellow Thingys: ";
	public string winText = "You Win!!!! Score!!: ";
	public string winMaybeText = "You Win??? Score??: ";
	public int winScore = 4;
	public int score = 0;

	private bool won = false;

	// Use this for initialization
	void Start () {
		
	}

	public void AddScore (int add) {
		score += add;
		won = false;
		textComponent.text = prefix + score;
	}

	public void Win (string nextLevel) {
		won = true;
		bool reallyWon = score >= winScore;
		if (reallyWon) {
			SceneManager.LoadScene(nextLevel);
		}
		textComponent.text = winMaybeText + score * 50;
	}
	
	// Update is called once per frame
	void Update () {
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
