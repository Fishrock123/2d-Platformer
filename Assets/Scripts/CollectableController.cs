using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour {

	public GameController gameController;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			gameController.AddScore(1);
			Destroy(gameObject);
		}
	}
}
