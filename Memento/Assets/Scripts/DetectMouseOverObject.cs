using UnityEngine;
using System.Collections;

public class DetectMouseOverObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	Ray ray;
	RaycastHit hit;

	void Update()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit))
		{
			print (hit.collider.name);
		}
	}
}
