using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

	public GameController gameController;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			gameController.Win();
		}
	}
}
