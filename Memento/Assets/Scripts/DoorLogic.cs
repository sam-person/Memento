using UnityEngine;
using System.Collections;

public class DoorLogic : MonoBehaviour {

	public Light[] myLights;
	public Color acceptColour;
	public Color offColour;
	public Animator animDoor;
	public Animator animValve;

	private int lightsGreen;
	private bool isOpen = false;

	// Use this for initialization
	void Start () {
		lightsGreen = 0;

		if (myLights.Length > 0) { // Check if Light array is not empty
			foreach (Light light in myLights) {
				light.color = offColour;
			}
		}

//		TurnValve ();
//		StartCoroutine (OpenSelectedDoor());
	}

	public void TurnNextLightGreen() {
		if (lightsGreen < myLights.Length) {
			myLights [lightsGreen].color = acceptColour;

			lightsGreen++;
		}
	}

	public void OpenDoor() {
		animDoor.Play ("DoorOpen");
		isOpen = true;
	}

	public void CloseDoor() {
		animDoor.Play ("DoorClose");
		isOpen = false;
	}

	public void TurnValve() {
		animValve.Play ("WheelRotate");
	}

	IEnumerator OpenDoorRoutine() {
		TurnValve ();
		yield return new WaitForSeconds (1.0f);
		OpenDoor ();

		yield return new WaitForSeconds (5.0f);
		CloseDoor ();
		StopCoroutine (OpenDoorRoutine ());
	}

	public void OpenSelectedDoor() {
		StartCoroutine (OpenDoorRoutine ());
	}

	public bool IsOpen {
		get { return isOpen; }
	}
}
