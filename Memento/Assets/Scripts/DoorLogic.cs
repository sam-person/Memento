using UnityEngine;
using System.Collections;

public class DoorLogic : MonoBehaviour {

	public Light[] myLights;
	public Color acceptColour;
	public Color offColour;
	public Animator animDoor;
	public Animator animValve;

	public AudioClip doorClose;
	public AudioClip doorOpen;
	public AudioClip turnValve;
	public AudioClip wrongDoor;

	public int order;
	public float audioCoolDown = 3.0f;

	AudioSource audio;

	private int lightsGreen;
	private bool isOpen = false;
	private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		lightsGreen = 0;
		audio = GetComponent<AudioSource> ();

		if (myLights.Length > 0) { // Check if Light array is not empty
			foreach (Light light in myLights) {
				light.color = offColour;
			}
		}

	}

	void Update() {
		timer += Time.deltaTime;
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

	public void IncorrectDoorSound() {
		if (audioCoolDown < timer) {
			PlaySound (wrongDoor);
			timer = 0.0f;
		}
	}

	public bool IsOpen {
		get { return isOpen; }
	}

	IEnumerator OpenDoorRoutine() {
		OpenValve ();
//		PlaySound (turnValve);
		yield return new WaitForSeconds (1.0f);
		PlaySound (doorOpen);
		OpenDoor ();

		StopCoroutine (OpenDoorRoutine ());
	}

	IEnumerator CloseDoorRoutine() {
		yield return new WaitForSeconds (Random.Range (0.0f, 0.5f));
		CloseDoor ();
		PlaySound (doorClose);
		yield return new WaitForSeconds (0.5f);
//		PlaySound (turnValve);
		CloseValve ();

		StopCoroutine (CloseDoorRoutine ());
	}

	void PlaySound(AudioClip clip) {
		audio.Stop ();
		audio.clip = clip;
		audio.Play ();
	}
}
