using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] fillerObjects;
	public GameObject[] keyObjects;
	public float amountOfFiller;
	public bool spawnFiller;

	public Color fogColour; // Fog
	public float density;
	public bool fogOn;

	// Use this for initialization
	void Start () {
		if (spawnFiller) {
			SpawnFillerObjects ();
		}
		SpawnKeyObjects();

		RenderSettings.fog = fogOn;
		RenderSettings.fogColor = fogColour;
		RenderSettings.fogDensity = density;
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

	void SpawnKeyObjects() {
		for (int i = 0; i < keyObjects.Length; i++) {
			GameObject keyObject = keyObjects [i];
			Quaternion spawnRotation = Quaternion.identity;
			Vector3 spawnPos = new Vector3 (0.0f, 3.0f, 0.0f);

			Instantiate (keyObject, spawnPos, spawnRotation);
		}
	}
}
