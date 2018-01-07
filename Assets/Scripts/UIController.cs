using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
	public GameController gameController;

	void Start() {
		// _musicSlider = GameObject.Find("Music_Slider").GetComponent<Slider>();
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetButtonDown("Menu")) {
			gameController.TogglePauseMenu();
		}
	}

	// //-----------------------------------------------------------
	// // Game Options Function Definitions
	// public void OptionSliderUpdate(float val) { ... }
	// void SetCustomSettings(bool val) { ... }
	// void WriteSettingsToInputText(GameSettings settings) { ... }

	// //-----------------------------------------------------------
	// // Music Settings Function Definitions
	// public void MusicSliderUpdate(float val)
	// {
	// 	MM.SetVolume(val);
	// }

	// public void MusicToggle(bool val)
	// {
	// 	_musicSlider.interactable = val;
	// 	MM.SetVolume(val ? _musicSlider.value : 0f);
	// }
}