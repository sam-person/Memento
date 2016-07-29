using UnityEngine;
using System.Collections;

public enum Keys { Wine_Bottle, Preg_Test, Teddy_Bear, Wilt_Flower, Car_Keys }

public class DetectKeyObject : MonoBehaviour {

//	public GameObject myLid;
//	public Collider myBase;
	public Animator animBottom;
	public Animator animTop;

	public float waitTime;

	public Keys matchKey;

	GameObject playerCam;
	GameObject containedObject;
	Collider myCollider;
	Vector3 initVel;


//	GameObject gameControllerObject;
//	GameController gameControllerScript;

	bool containsObject;
	bool acceptedObject;
	bool coroutineActive;

	// Use this for initialization
	void Start () {
//		gameControllerObject = GameObject.FindGameObjectWithTag ("GameController"); // get GameController Reference
//		gameControllerScript = gameControllerObject.GetComponent<GameController> ();

		playerCam = GameObject.FindGameObjectWithTag ("MainCamera");
		myCollider = GetComponent<Collider> ();
		containsObject = false;
		acceptedObject = false;
		coroutineActive = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (containsObject && !acceptedObject) { 
			if (!PlayerHoldingObject()) { // Check if player is still holding object
				if (!coroutineActive) {
					StartCoroutine (CheckObject ());
					coroutineActive = true;
				}

				if (!containsObject) {
//					myBase.enabled = true;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("KeyObject") && !acceptedObject) {
			containsObject = true;

			containedObject = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("KeyObject")) {
			containsObject = false;

//			myLid.SetActive (false);
			acceptedObject = false;
			containedObject = null;
		}
	}

	IEnumerator CheckObject() {
		yield return new WaitForSeconds (waitTime);

		if (containsObject && !PlayerHoldingObject()) {
			Rigidbody rb = containedObject.GetComponent<Rigidbody> ();

			if (!acceptedObject) {
				rb.velocity += new Vector3 (0.0f, -0.25f, 0.0f);
			}
			
//			eBlue key = containedObject.GetComponent<eBlue> (); // KEY
			KeyObjectScript keyScript = containedObject.GetComponent<KeyObjectScript>();
			if (keyScript.MyKey == matchKey) {
//			if (key != null) {
				StopCoroutine (CheckObject ());
//				myLid.SetActive (true);
//				myBase.enabled = true;

				GameController.current.CorrectObjectInserted ();
				acceptedObject = true;

				myCollider.enabled = false; // Turn object and collider off
				containedObject.SetActive (false);

				coroutineActive = false;
				animTop.Play ("hatchClose2"); // Close the hatch
				animBottom.Play ("hatchClose1");
			} else {
//				myBase.enabled = false;
				StopCoroutine(CheckObject());
				coroutineActive = false;
			}
		}
		StopCoroutine(CheckObject());
		coroutineActive = false;
	}

	bool PlayerHoldingObject() {
		DetectMouseOverObject playerCamScript = playerCam.GetComponent<DetectMouseOverObject> ();
		return playerCamScript.isPlayerHoldingObject;
	}


}
