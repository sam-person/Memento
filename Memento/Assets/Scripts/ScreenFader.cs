using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour {

	public static ScreenFader current;

	public float fadeOutSpeed = 1.0f;
	public float fadeInSpeed = 1.5f;
	public string scene;

	Image myImage;

	private bool sceneStarting;

	// Use this for initialization
	void Start () {
		current = this;
		sceneStarting = true;

		myImage = GetComponent<Image> ();

		myImage.rectTransform.sizeDelta = new Vector2 (Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		if (sceneStarting) {
			StartScene ();
		}
	}

	void FadeToClear() {
		myImage.color = Color.Lerp (myImage.color, Color.clear, fadeInSpeed * Time.deltaTime);
	}

	void FadeToBlack() {
		myImage.color = Color.Lerp (myImage.color, Color.black, fadeOutSpeed * Time.deltaTime);
	}

	public void StartScene() {
		FadeToClear ();

		if (myImage.color.a <= 0.05f) {
			myImage.color = Color.clear;
			myImage.enabled = false;

			sceneStarting = false;
		}
	}

	public void EndScene() {
		myImage.enabled = true;

		FadeToBlack ();

		if (myImage.color.a <= 0.95f) {
			myImage.color = Color.black;

			SceneManager.LoadScene (scene);
		}
	}
}
