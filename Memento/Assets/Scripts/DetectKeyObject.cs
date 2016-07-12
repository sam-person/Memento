using UnityEngine;
using System.Collections;

public class DetectKeyObject : MonoBehaviour {

	public GameObject myLid;
	public Collider myBase;

	public float waitTime;

	GameObject playerCam;
	GameObject containedObject;

	bool containsObject;
	bool acceptedObject;

	// Use this for initialization
	void Start () {
		playerCam = GameObject.FindGameObjectWithTag ("MainCamera");
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
		if (other.CompareTag("KeyObject")) {
			containsObject = true;
			Debug.Log ("Object is in");

			containedObject = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("KeyObject")) {
			containsObject = false;
			Debug.Log ("Object is out");

			myLid.SetActive (false);
			acceptedObject = false;
			containedObject = null;
		}
	}

	IEnumerator CheckObject() {
		yield return new WaitForSeconds (2.0f);

		if (containsObject && !PlayerHoldingObject()) {
			
			eBlue key = containedObject.GetComponent<eBlue> ();
			if (key != null) {
				myLid.SetActive (true);
				acceptedObject = true;

			} else {
				myBase.enabled = false;
				yield return new WaitForSeconds (1.0f);
				myBase.enabled = true;

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
