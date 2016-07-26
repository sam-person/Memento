using UnityEngine;
using System.Collections;

public class Floating2 : MonoBehaviour {

	public float tumble = 0.15f;
	public float capVel = 2.0f;
	public float capAngVel = 2.0f;
	public float maxHeight = 10.0f;
	public float minHeight = 0.05f;
	public float bounceForce = 0.05f;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		rb.angularVelocity = Random.insideUnitSphere * tumble;
		rb.maxAngularVelocity = capAngVel;

		if (this.CompareTag ("KeyObject")) {
			maxHeight = maxHeight / 2.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = new Vector3 (
			Mathf.Clamp (rb.velocity.x, -capVel, capVel),
			Mathf.Clamp (rb.velocity.y, -capVel, capVel),
			Mathf.Clamp (rb.velocity.z, -capVel, capVel));

		if (rb.transform.position.y > maxHeight) {
			rb.velocity = new Vector3 (rb.velocity.x, -1.0f, rb.velocity.z);
		} else if (rb.transform.position.y < minHeight) {
			rb.velocity = new Vector3 (rb.velocity.x, 0.1f, rb.velocity.z);
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag("Wall")) {
			Vector3 toMiddle = -transform.position;

			rb.AddForce(toMiddle * bounceForce);
		}
	}
}
