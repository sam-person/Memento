using UnityEngine;
using System.Collections;

public class DetectKeyObjectYellow : MonoBehaviour {

	public GameObject myLid;
	public Collider myBase;

	public float waitTime;

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
			if (!PlayerHoldingObject()) {
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
			
			eYellow key = containedObject.GetComponent<eYellow> (); // KEY
			if (key != null) {
				StopCoroutine (CheckObject ());
//				myLid.SetActive (true);
//				myBase.enabled = true;

				GameController.current.CorrectObjectInserted ();
				acceptedObject = true;

				myCollider.enabled = false;
				containedObject.SetActive (false);

				coroutineActive = false;
			} else {
//				myBase.enabled = false;
				StopCoroutine(CheckObject());
				coroutineActive = false;
			}
		}
	}

	bool PlayerHoldingObject() {
		DetectMouseOverObject playerCamScript = playerCam.GetComponent<DetectMouseOverObject> ();
		return playerCamScript.isPlayerHoldingObject;
	}


}
