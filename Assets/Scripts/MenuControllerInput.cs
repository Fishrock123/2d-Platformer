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

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable () {
		selected = 0;
		incrementTimer = 0;

		StartCoroutine(SelectButtonAsync());
	}
 
	IEnumerator SelectButtonAsync () {
		// XXX: Awful hack to make the button re-select.
		// TODO: Should probably just spawn in a new pause menu each time.
		// See https://answers.unity.com/questions/1142958/buttonselect-doesnt-highlight.html
		yield return null;
    	if (buttons.Count > 0) {
			buttons[selected + 1].Select();
			buttons[selected].Select();
		}
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
