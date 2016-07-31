using UnityEngine;
using System.Collections;

public class TestSounds : MonoBehaviour {

	public AudioClip clip1;
	public AudioClip clip2;
	public AudioClip clip3;
	public AudioClip clip4;
	public AudioClip clip5;

	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("1")) {
			PlaySound (clip1);
		} else if (Input.GetKeyDown ("2")) {
			PlaySound (clip2); // Door 2
		} else if (Input.GetKeyDown ("3")) {
			PlaySound (clip3);
		} else if (Input.GetKeyDown ("4")) {
			PlaySound (clip4);
		} else if (Input.GetKeyDown ("5")) {
			PlaySound (clip5);
		}
	}

	void PlaySound(AudioClip clip, float pitch = 1.0f) {
		audio.Stop ();
		audio.clip = clip;
		audio.pitch = pitch;
		audio.Play ();

	}
}
