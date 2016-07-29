using UnityEngine;
using System.Collections;

public class OutsideBounds : MonoBehaviour {

	public Vector3 respawnCentre;

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("KeyObject")) {
			other.transform.position = respawnCentre;
			Rigidbody rb = other.GetComponent<Rigidbody> ();
			rb.velocity += new Vector3 (0.0f, -0.25f, 0.0f);
		}
	}
}
