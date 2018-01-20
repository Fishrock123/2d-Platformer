using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageEndMenu : MonoBehaviour {
	public GameController gameController;

	private string nextLevel;

	public void SetNextLevel (string level) {
		nextLevel = level;
	}

	public void ContinueToNext () {
		gameController.LoadLevel(nextLevel);
	}

	public void ReplayCurrent () {
		gameController.LoadLevel(SceneManager.GetActiveScene().name);
	}
}
