using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandAction : MonoBehaviour {
	public LineRenderer raycast;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.Get (OVRInput.Touch.SecondaryIndexTrigger, OVRInput.Controller.Touch) == false) {
			raycast.enabled = true;
			RaycastHit hit;
			Vector3 fwd = transform.TransformDirection (Vector3.forward);
			Vector3 endPosition = transform.position;
			if (Physics.Raycast (transform.position, fwd, out hit, 10000)) {
				endPosition = hit.point;


			}
			raycast.SetPosition (0, transform.position);
			raycast.SetPosition (1, endPosition);
		} else {
			raycast.enabled = false;
		}
	}
}
