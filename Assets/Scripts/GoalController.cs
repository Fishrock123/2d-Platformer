using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

	public GameController gameController;
	public string nextLevel;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			gameController.Win(nextLevel);
		}
	}
}
