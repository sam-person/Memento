using UnityEngine;
using System.Collections;

public class animations1 : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("2")) {
			anim.Play ("hatchClose1");
		}
	}
}
