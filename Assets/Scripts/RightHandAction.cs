using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandAction : MonoBehaviour {
	public LineRenderer raycast;
	GameObject chosen;
	Transform chosenparent;
	bool isGrabbed = false;
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
			if (isGrabbed == false) {
				if (Physics.Raycast (transform.position, fwd, out hit, 10000)) {
					endPosition = hit.point;
					chosen = hit.collider.gameObject;
					if (chosen.GetComponent<Rigidbody> () != null) {
						if (OVRInput.Get (OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) == 1.0f) {
							isGrabbed = true;
							chosen.GetComponent<Rigidbody> ().isKinematic = true;
							chosenparent = chosen.transform.parent;
							chosen.transform.parent = transform;
							//chosen.transform.position = transform.position - transform.forward;
						} else {
							isGrabbed = false;
							chosen.transform.parent = null;
							chosen.GetComponent<Rigidbody> ().isKinematic = false;
						}
					}

				}
			}
			raycast.SetPosition (0, transform.position);
			raycast.SetPosition (1, endPosition);
		} else {
			raycast.enabled = false;
		}
	}
}
