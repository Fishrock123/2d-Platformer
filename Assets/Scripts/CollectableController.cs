using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour {

	public GameController gameController;
	private bool collected = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !collected) {
			collected = true;
			gameController.AddScore(1);
			Destroy(gameObject);
		}
	}
}
