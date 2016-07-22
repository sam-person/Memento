using UnityEngine;
using System.Collections;

public class DetectKeyObject : MonoBehaviour {

	public GameObject myLid;
	public Collider myBase;

	public float waitTime;

	GameObject playerCam;
	GameObject containedObject;
	Collider myCollider;
	Vector3 initVel;
	Animator anim;

//	GameObject gameControllerObject;
//	GameController gameControllerScript;

	bool containsObject;
	bool acceptedObject;

	// Use this for initialization
	void Start () {
//		gameControllerObject = GameObject.FindGameObjectWithTag ("GameController"); // get GameController Reference
//		gameControllerScript = gameControllerObject.GetComponent<GameController> ();

		playerCam = GameObject.FindGameObjectWithTag ("MainCamera");
		myCollider = GetComponent<Collider> ();
		anim = GetComponent<Animator> ();
		containsObject = false;
		acceptedObject = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (containsObject && !acceptedObject) {
			if (!PlayerHoldingObject()) {
				StartCoroutine (CheckObject ());

				if (!containsObject) {
//					myBase.enabled = true;
				}
			}
		}

		if (Input.GetKeyDown ("1")) {
			anim.Play ("Gap close");
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
				rb.velocity += new Vector3 (0.0f, -0.5f, 0.0f);
			}
			
			eBlue key = containedObject.GetComponent<eBlue> (); // KEY
			if (key != null) {
//				myLid.SetActive (true);
				acceptedObject = true;
//				myBase.enabled = true;
//				myCollider.enabled = false;
//				containedObject.SetActive (false);

			} else {
//				myBase.enabled = false;

			}
		}
	}

	bool PlayerHoldingObject() {
		DetectMouseOverObject playerCamScript = playerCam.GetComponent<DetectMouseOverObject> ();
		return playerCamScript.isPlayerHoldingObject;
	}


}
