using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEndMenu : MonoBehaviour {
	public GameController gameController;

	private string nextLevel;

	public void SetNextLevel (string level) {
		nextLevel = level;
	}

	public void ContinueToNext () {
		gameController.LoadLevel(nextLevel);
	}
}
