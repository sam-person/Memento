using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetectMouseOverObject : MonoBehaviour {

	public Image hud;
	public Sprite normal;
	public Sprite openHand;
	public Sprite closeHand;

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
//			print (hit.collider.tag);
			if (hit.collider.CompareTag ("KeyObject")) { // Check object's tag
				if (Input.GetButton ("Fire1")) { // Check if player is clicking
					hud.sprite = closeHand;
				} else { // Player is JUST looking at object
					hud.sprite = openHand;
				}
			} else { // Show cross hair instead
				hud.sprite = normal;
			}
		}
	}
}
