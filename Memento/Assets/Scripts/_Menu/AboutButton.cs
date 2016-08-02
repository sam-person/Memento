using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AboutButton : MonoBehaviour {
	public GameObject storyText;
	public GameObject creditsText;

	private bool buttonState;
	private int count;
	private int requiredClicks = 20;

	void Start() {
		storyText.SetActive (false);
		creditsText.SetActive (false);

		buttonState = false;

	}

	public void ClickedButton() {
		count++;
		buttonState = !buttonState;
		ShowText ();
	}

	void ShowText() {
		if (buttonState) {
			creditsText.SetActive (true);
			if (count >= requiredClicks) {
				storyText.SetActive (true);
			}
		} else {
			creditsText.SetActive (false);
			storyText.SetActive (false);
		}
	}
}
