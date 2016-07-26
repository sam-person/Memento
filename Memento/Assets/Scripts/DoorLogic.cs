using UnityEngine;
using System.Collections;

public class DoorLogic : MonoBehaviour {

	public Light[] myLights;
	public Color acceptColour;
	public Color offColour;
	public Animator animDoor;
	public Animator animValve;

	public int order;

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

	public void OpenValve() {
		animValve.Play ("WheelRotate");
	}

	public void CloseValve() {
		animValve.Play ("WheelClose");
	}
		

	public void OpenSelectedDoor() {
		StartCoroutine (OpenDoorRoutine ());
	}

	public void CloseSelectedDoor() {
		StartCoroutine (CloseDoorRoutine ());
	}

	public bool IsOpen {
		get { return isOpen; }
	}

	IEnumerator OpenDoorRoutine() {
		OpenValve ();
		yield return new WaitForSeconds (1.0f);
		OpenDoor ();

		StopCoroutine (OpenDoorRoutine ());
	}

	IEnumerator CloseDoorRoutine() {
		CloseDoor ();
		yield return new WaitForSeconds (0.5f);
		CloseValve ();

		StopCoroutine (CloseDoorRoutine ());
	}
}
