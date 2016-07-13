using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] fillerObjects;
	public GameObject[] keyObjects;
	public float amountOfFiller;

	// Use this for initialization
	void Start () {
		SpawnFillerObjects ();
		for (int i = 0; i < keyObjects.Length; i++) {
			GameObject keyObject = keyObjects [i];
			Quaternion spawnRotation = Quaternion.identity;
			Vector3 spawnPos = new Vector3 (0.0f, 3.0f, 0.0f);

			Instantiate (keyObject, spawnPos, spawnRotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnFillerObjects() {
		for (int i = 0; i < amountOfFiller; i++) {
			GameObject filler = fillerObjects [Random.Range (0, fillerObjects.Length)];
			Quaternion spawnRotation = Quaternion.identity;
			Vector3 spawnPos = new Vector3 (0.0f, 3.0f, 0.0f);

			Instantiate (filler, spawnPos, spawnRotation);
		}
	}
}
