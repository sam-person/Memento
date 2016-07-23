using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController current;

	public GameObject[] fillerObjects; // Objects
	public GameObject[] keyObjects;
	public float amountOfFiller;
	public bool spawnFiller;
	public float spawnHeight;

	public Color fogColour; // Fog
	public float density;
	public bool fogOn;

	GameObject[] Doors;
	int insertedKeysCount = 0;

	// Use this for initialization
	void Start () {
		current = this;

		if (spawnFiller) {
			SpawnFillerObjects ();
		}
//		SpawnKeyObjects();

		RenderSettings.fog = fogOn;
		RenderSettings.fogColor = fogColour;
		RenderSettings.fogDensity = density;



		current.Doors = GameObject.FindGameObjectsWithTag ("Door");
//		StartCoroutine (current.TestLights ());

	}

	public void SpawnFillerObjects() {
		for (int i = 0; i < amountOfFiller; i++) {
			GameObject filler = fillerObjects [Random.Range (0, fillerObjects.Length)];
			Quaternion spawnRotation = Quaternion.identity;
			Vector3 spawnPos = new Vector3 (0.0f, spawnHeight, 0.0f);

			Instantiate (filler, spawnPos, spawnRotation);
		}
	}

	public void SpawnKeyObjects() {
		for (int i = 0; i < keyObjects.Length; i++) {
			GameObject keyObject = keyObjects [i];
			Quaternion spawnRotation = Quaternion.identity;
			Vector3 spawnPos = new Vector3 (0.0f, spawnHeight, 0.0f);

			Instantiate (keyObject, spawnPos, spawnRotation);
		}
	}


	public IEnumerator TestLights() {
		for (int j = 0; j < 5; j++) {
			yield return new WaitForSeconds (1.0f);

			if (current.Doors.Length > 0) {
				for (int i = 0; i < current.Doors.Length; i++) {
					DoorLogic currentDoor = current.Doors [i].GetComponent<DoorLogic> ();

					if (currentDoor != null) {
						currentDoor.TurnNextLightGreen ();
					}
				}
			}
		}

		StopCoroutine (TestLights ());
	}

	public void CorrectObjectInserted() {
//		Debug.Log ("inserted: " + current.insertedKeysCount + " doors: " + current.Doors.Length);
//		if (current.insertedKeysCount < current.Doors.Length) {
//			
//			current.insertedKeysCount++;
//		}
		for (int i = 0; i < current.Doors.Length; i++) {
			DoorLogic currentDoor = current.Doors [i].GetComponent<DoorLogic> ();
			currentDoor.TurnNextLightGreen ();
		}

	}
}
