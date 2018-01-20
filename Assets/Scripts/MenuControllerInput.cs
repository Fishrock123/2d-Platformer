using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControllerInput : MonoBehaviour {

	public List<Button> buttons;
	public Button cancelOrResume;
	public float incrementTime = 200f;

	private int selected = 0;
	private float incrementTimer = 0;

	// Use this for initialization
	void Start () {
		if (buttons.Count > 0) {
			buttons[selected].Select();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		float hDir = Input.GetAxisRaw("Vertical");
		if (hDir != 0) {
			incrementTimer += Time.unscaledDeltaTime;
		} else {
			incrementTimer = incrementTime;
		}
		if (incrementTimer >= incrementTime) {
			if (hDir > 0) {
				Increment();
				incrementTimer = 0;
			} else if (hDir < 0) {
				Decrement();
				incrementTimer = 0;
			}
		}

		if (Input.GetButton("Submit")) {
			buttons[selected].onClick.Invoke();
		} else if (Input.GetButton("Cancel")) {
			cancelOrResume.onClick.Invoke();
		}
	}

	void Increment () {
		if (buttons.Count > 0) {
			selected += 1;
			if (selected >= buttons.Count) selected = 0;
			buttons[selected].Select();
		}
	}

	void Decrement () {
		if (buttons.Count > 0) {
			selected -= 1;
			if (selected < 0) selected = buttons.Count - 1;
			buttons[selected].Select();
		}
	}
}
