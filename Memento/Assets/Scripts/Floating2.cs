using UnityEngine;
using System.Collections;

public class Floating2 : MonoBehaviour {

	public float tumble = 0.15f;
	public float capVel = 2.0f;
	public float capAngVel = 2.0f;
	public float maxHeight = 5.0f;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		rb.angularVelocity = Random.insideUnitSphere * tumble;
		rb.maxAngularVelocity = capAngVel;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = new Vector3 (
			Mathf.Clamp (rb.velocity.x, -capVel, capVel),
			Mathf.Clamp (rb.velocity.y, -capVel, capVel),
			Mathf.Clamp (rb.velocity.z, -capVel, capVel));

		if (rb.transform.position.y > maxHeight) {
			rb.velocity = new Vector3 (rb.velocity.x, -1.0f, rb.velocity.z);
		}
	}
}
