using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {

//	public float speed;
//	public float amplitude;
//	public float min;
//	public float max;
//
//	float y0;

	public float force;
	public float bobTick;
	public float bobTime;
	public float maxVel;
	public float tumble;

	Rigidbody rb;
	bool bobUp;
	bool startingVel;
	int count;
	int sign;

	// Use this for initialization
	void Start () {
		bobUp = true;
		startingVel = true;
		count = 60;

		rb = GetComponent<Rigidbody> ();

		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
	
	// Update is called once per frame
	void Update () {
//		y0 = transform.position.y;
//
//		float newy = Mathf.Clamp (y0 + amplitude * Mathf.Sin (speed * Time.deltaTime), min, max);
//		transform.position = new Vector3 (transform.position.x, 
//			newy, 
//			transform.position.z);


		if (rb.velocity.y <= -maxVel) {
			Debug.Log ("Going UP");
			sign = 1;
		} else if (rb.velocity.y >= maxVel) {
			Debug.Log ("Going DOWN-----");
			sign = -1;
		}

		if (startingVel) {
			rb.AddForce (transform.up * force);
			if (rb.velocity.y >= maxVel) {
				startingVel = false;
			}
		} else {
			rb.AddForce (sign * transform.up * force);
		}

		Debug.Log (rb.velocity.y + " count: " + count);

		count += 1;
	}
		
}
