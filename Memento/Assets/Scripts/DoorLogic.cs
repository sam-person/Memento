using UnityEngine;
using System.Collections;

public class DoorLogic : MonoBehaviour {

	public Light[] myLights;
	public Color acceptColour;
	public Color offColour;

	private int lightsGreen;

	// Use this for initialization
	void Start () {
		lightsGreen = 0;

		if (myLights.Length > 0) { // Check if Light array is not empty
			foreach (Light light in myLights) {
				light.color = offColour;
			}
		}
	}

	public void TurnNextLightGreen() {
		if (lightsGreen < myLights.Length) {
			myLights [lightsGreen].color = acceptColour;

			lightsGreen++;
		}
	}

	public void OpenDoor() {

	}

	public void CloseDoor() {

	}
}
