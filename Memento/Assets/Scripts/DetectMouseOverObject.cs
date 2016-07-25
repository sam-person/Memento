using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetectMouseOverObject : MonoBehaviour {

	public static DetectMouseOverObject current;

	public Image hud;
	public Sprite normal;
	public Sprite openHand;
	public Sprite closeHand;

	public float distanceFromCam;
	public float pickUpRange;
	public float smooth;

	private bool isCarrying;

	GameObject carriedObject;
	Camera mainCamera;

	Ray ray;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		current = this;
		isCarrying = false;
		mainCamera = GetComponent<Camera> ();
	}

	void Update()
	{
		if (!GameController.current.isPhase2) {
			// Check if player is carrying an object
			if (isCarrying) {
				Carry (carriedObject);
				CheckDropObject (); // check if player wants to drop object
			} else {
				PickUp ();
			}
		} else {
			LookAt ();
		}

	}

	// pick up target object
	void PickUp() {
		ray = mainCamera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast (ray, out hit)) {
			if (hit.distance <= pickUpRange) {
				if (hit.collider.CompareTag ("KeyObject")) { // Check object's tag
					if (Input.GetButtonDown ("Fire1")) { // Check if player is clicking
						hud.sprite = closeHand;
						Rigidbody rbCarriedObject = hit.collider.GetComponent<Rigidbody> ();

						if (rbCarriedObject != null) {
							isCarrying = true;
							carriedObject = rbCarriedObject.gameObject;
//							rbCarriedObject.useGravity = false;
						}
					} else { // Player is JUST looking at object
						hud.sprite = openHand;
					}
				} else {
					if (carriedObject != null) {
						DropObject ();
					}
					hud.sprite = normal;
				}
			} else {
				hud.sprite = normal;
			}
		} else { // Default to normal cross hair
			hud.sprite = normal;
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
				mainCamera.transform.position + mainCamera.transform.forward * distanceFromCam,
				Time.deltaTime * smooth);

		// prevent rotation
		heldObject.transform.rotation = Quaternion.identity;
	}

	// Check if player wants to drop object by clicking while holding an object
	void CheckDropObject() {
		if (Input.GetButtonUp ("Fire1")) { // Check if player is clicking
			DropObject();
		}
	}

	void DropObject() {
		isCarrying = false; // turn off carrying
		
		// Turn Gravity on
		Rigidbody rbCarriedObject = carriedObject.GetComponent<Rigidbody> ();

		rbCarriedObject.velocity = Vector3.zero; // reset velocity
		rbCarriedObject.velocity = new Vector3(0.0f, -0.5f, 0.0f);

		carriedObject = null; // reset object
	}

	// Check if player is holding an object
	public bool isPlayerHoldingObject {
		
		get { return isCarrying; }
	}

	void LookAt() {
		ray = mainCamera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast (ray, out hit)) {
			if (hit.distance <= pickUpRange) {
				if (hit.collider.CompareTag ("Door")) { // Check object's tag
					if (Input.GetButton ("Fire1")) { // Check if player is clicking
						hud.sprite = closeHand;
//						Rigidbody rbCarriedObject = hit.collider.GetComponent<Rigidbody> ();
						DoorLogic door = hit.collider.GetComponent<DoorLogic> ();
						Debug.Log (door);
//						GameObject doorParent = door.transform.parent.gameObject;
						door.OpenSelectedDoor();

					} else { // Player is JUST looking at object
						hud.sprite = openHand;
					}
				} else {
					hud.sprite = normal;
				}
			} else {
				hud.sprite = normal;
			}
		} else { // Default to normal cross hair
			hud.sprite = normal;
		}
	}

}
