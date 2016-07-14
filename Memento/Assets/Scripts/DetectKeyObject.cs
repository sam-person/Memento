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

	bool containsObject;
	bool acceptedObject;

	// Use this for initialization
	void Start () {
		playerCam = GameObject.FindGameObjectWithTag ("MainCamera");
		myCollider = GetComponent<Collider> ();
		containsObject = false;
		acceptedObject = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (containsObject && !acceptedObject) {
			if (!PlayerHoldingObject()) {
				StartCoroutine (CheckObject ());
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

			myLid.SetActive (false);
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
			
			eBlue key = containedObject.GetComponent<eBlue> ();
			if (key != null) {
				myLid.SetActive (true);
				acceptedObject = true;
				myBase.enabled = true;

			} else {
				myBase.enabled = false;
				if (!containsObject) {
					myBase.enabled = true;
				}
			}
		}
	}

	bool PlayerHoldingObject() {
		DetectMouseOverObject playerCamScript = playerCam.GetComponent<DetectMouseOverObject> ();
		if (playerCamScript.isPlayerHoldingObject) {
			return true;
		} else {
			return false;
		}
	}
}
