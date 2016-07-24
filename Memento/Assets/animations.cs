using UnityEngine;
using System.Collections;

public class animations : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("1")) {
			anim.Play ("hatchClose2");
		}
	}
}
