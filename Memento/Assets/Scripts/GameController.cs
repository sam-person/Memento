using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController current;
	public bool completedGame;
	public bool isPhase2 = true;

	public GameObject[] fillerObjects; // Objects
	public GameObject[] keyObjects;
	public float amountOfFiller;
	public bool spawnFiller;
	public float spawnHeight;

	public Color fogColour; // Fog
	public float density;
	public bool fogOn;

	GameObject[] Doors;
	DoorLogic[] doorLogics = new DoorLogic[5];
	int numOfKeys;
	int insertedKeysCount = 0;


	// Use this for initialization
	void Start () {
		current = this;

		if (spawnFiller) {
			SpawnFillerObjects ();
		}
		SpawnKeyObjects();

		RenderSettings.fog = fogOn;
		RenderSettings.fogColor = fogColour;
		RenderSettings.fogDensity = density;


		Doors = GameObject.FindGameObjectsWithTag ("Door");
		for (int i = 0; i < Doors.Length; i++) {
			doorLogics [i] = Doors [i].GetComponent<DoorLogic> ();
		}
		numOfKeys = Doors.Length;

	}

	void Update() {
		// Check for phase 2 (i.e. door opening puzzle)
		if (!isPhase2) {
			if (insertedKeysCount >= numOfKeys) {
				isPhase2 = true;
			}
		}

		// Player manual exit
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("TitleMenu");
		}

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
		

	public void CorrectObjectInserted() {
		for (int i = 0; i < Doors.Length; i++) {
			doorLogics[i].TurnNextLightGreen ();
		}
		insertedKeysCount++;
	}

	public bool CheckDoorOrder(int doorNumber) {
		
		bool correctDoorOrdering = true;
		for (int i = 0; i < doorLogics.Length; i++) {
			if (doorLogics [i].order < doorNumber) { // Check if current door is before given door
				if (correctDoorOrdering) { // Check if door order is still correct
					if (doorLogics [i].IsOpen) { // Check if prior door has been opened
						correctDoorOrdering = true;

					// Incorrect Door
					} else {
						correctDoorOrdering = false;
					}
				}
			}

			if (!correctDoorOrdering) {
				if (doorLogics [i].order == doorNumber) {
					doorLogics [i].IncorrectDoorSound ();
				}
			}
		}

		if (!correctDoorOrdering) {
			CloseAllDoors ();
		}

		completedGame = CheckAllDoorsOpen (); // Check if all doors have been opened

		if (completedGame) {
			StartCoroutine (EndGame ());
		}

		return correctDoorOrdering;
	}

	public void CloseAllDoors() {
		for (int i = 0; i < Doors.Length; i++) {
			if (doorLogics [i].IsOpen) {
				doorLogics [i].CloseSelectedDoor ();
			}
		}
	}

	bool CheckAllDoorsOpen() {
		int count = 0;
		foreach(DoorLogic logic in doorLogics) {
			if(logic.IsOpen) {
				count++;
			}
		}

		if (count >= doorLogics.Length - 1) {
			return true;
		} else {
			return false;
		}
	}

	IEnumerator EndGame() {
		yield return new WaitForSeconds (20.0f);
		ScreenFader.current.EndScene ();

		StopCoroutine (EndGame ());
	}
}
