using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] fillerObjects; // Objects
	public GameObject[] keyObjects;
	public float amountOfFiller;
	public bool spawnFiller;
	public float spawnHeight;

	public Color fogColour; // Fog
	public float density;
	public bool fogOn;

	GameObject[] Doors;

	// Use this for initialization
	void Start () {
		if (spawnFiller) {
			SpawnFillerObjects ();
		}
//		SpawnKeyObjects();

		RenderSettings.fog = fogOn;
		RenderSettings.fogColor = fogColour;
		RenderSettings.fogDensity = density;



		Doors = GameObject.FindGameObjectsWithTag ("Door");
		StartCoroutine (TestLights ());

	}

	void SpawnFillerObjects() {
		for (int i = 0; i < amountOfFiller; i++) {
			GameObject filler = fillerObjects [Random.Range (0, fillerObjects.Length)];
			Quaternion spawnRotation = Quaternion.identity;
			Vector3 spawnPos = new Vector3 (0.0f, spawnHeight, 0.0f);

			Instantiate (filler, spawnPos, spawnRotation);
		}
	}

	void SpawnKeyObjects() {
		for (int i = 0; i < keyObjects.Length; i++) {
			GameObject keyObject = keyObjects [i];
			Quaternion spawnRotation = Quaternion.identity;
			Vector3 spawnPos = new Vector3 (0.0f, spawnHeight, 0.0f);

			Instantiate (keyObject, spawnPos, spawnRotation);
		}
	}


	IEnumerator TestLights() {
		for (int j = 0; j < 5; j++) {
			yield return new WaitForSeconds (1.0f);

			if (Doors.Length > 0) {
				for (int i = 0; i < Doors.Length; i++) {
					DoorLogic currentDoor = Doors [i].GetComponent<DoorLogic> ();

					if (currentDoor != null) {
						currentDoor.TurnNextLightGreen ();
					}
				}
			}
		}

		StopCoroutine (TestLights ());
	}

	public void CorretObjectInserted() {
//		if (keyObjectsInsertedCount < Doors.Length) {
//			DoorLogic currentDoor = Doors [keyObjectsInsertedCount].GetComponent<DoorLogic> ();
//			currentDoor.TurnNextLightGreen ();
//		}
//
//		keyObjectsInsertedCount++;

	}
}
