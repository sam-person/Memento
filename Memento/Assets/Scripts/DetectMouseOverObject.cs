﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetectMouseOverObject : MonoBehaviour {

	public Image hud;
	public Sprite normal;
	public Sprite openHand;
	public Sprite closeHand;

	public float distance;
	public float smooth;

	private bool isCarrying;

	GameObject carriedObject;
	Camera mainCamera;

	// Use this for initialization
	void Start () {
		isCarrying = false;
		mainCamera = GetComponent<Camera> ();
	}

	Ray ray;
	RaycastHit hit;

	void Update()
	{
		// Check if player is carrying an object
		if (isCarrying) {
			Carry (carriedObject);
			CheckDropObject (); // check if player wants to drop object
		} else {
			PickUp ();
		}

	}

	// pick up target object
	void PickUp() {
		ray = mainCamera.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(ray, out hit)) {
			if (hit.collider.CompareTag ("KeyObject")) { // Check object's tag
				if (Input.GetButtonDown ("Fire1")) { // Check if player is clicking
					hud.sprite = closeHand;
					Rigidbody rbCarriedObject = hit.collider.GetComponent<Rigidbody> ();

					if (rbCarriedObject != null) {
						isCarrying = true;
						carriedObject = rbCarriedObject.gameObject;
						rbCarriedObject.useGravity = false;
					}
				} else { // Player is JUST looking at object
					hud.sprite = openHand;
				}
			} else { // Show cross hair instead
				hud.sprite = normal;
			}
		}

	}

	/// <summary>
	/// Carry the specified heldObject.
	/// </summary>
	/// <param name="heldObject">The object to be carried.</param>
	void Carry(GameObject heldObject) {
		// Move object to middle of screen
		heldObject.transform.position = 
			Vector3.Lerp(heldObject.transform.position, 
				mainCamera.transform.position + mainCamera.transform.forward * distance,
				Time.deltaTime * smooth);

		// prevent rotation
		heldObject.transform.rotation = Quaternion.identity;
	}

	// Check if player wants to drop object by clicking while holding an object
	void CheckDropObject() {
		if (Input.GetButtonUp ("Fire1")) { // Check if player is clicking
			isCarrying = false; // turn off carrying

			// Turn Gravity on
			Rigidbody rbCarriedObject = carriedObject.GetComponent<Rigidbody> ();
			if (rbCarriedObject != null) { 
				rbCarriedObject.useGravity = true;
			}

			rbCarriedObject.velocity = Vector3.zero; // reset velocity

			carriedObject = null; // reset object
		}
	}
}
