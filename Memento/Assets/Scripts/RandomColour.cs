using UnityEngine;
using System.Collections;

public class RandomColour : MonoBehaviour {

	GameObject me;
	Renderer myRenderer;

	// Use this for initialization
	void Start () {
		me = GetComponent<GameObject> ();
		myRenderer = GetComponent<Renderer> ();

		Color randColor = new Color (Random.value, Random.value, Random.value, 1.0f);

		myRenderer.material.color = randColor;

		StartCoroutine (ChangeColour ());
	}

	IEnumerator ChangeColour() {
		while (true) {
			float randWaitTime = Random.Range (1.0f, 3.0f);

			yield return new WaitForSeconds (randWaitTime);

			Color randColor = new Color (Random.value, Random.value, Random.value, 1.0f);

			myRenderer.material.color = randColor;
		}
	}
}
