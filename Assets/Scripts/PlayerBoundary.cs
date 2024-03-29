﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoundary : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		// Reset player position
		if (other.tag == "Player") {
			// other.GetComponent<Rigidbody2D>().position = new Vector3(0f, 0f, 0f);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			return;
		}

		// Destroy everything that leaves the trigger
		Destroy(other.gameObject);
	}
}
