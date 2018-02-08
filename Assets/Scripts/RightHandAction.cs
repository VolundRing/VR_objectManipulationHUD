using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandAction : MonoBehaviour {
	public LineRenderer raycast;
	GameObject chosen;
	public SphereCollider thisCollider;
	Transform chosenparent;
	bool isGrabbed = false;
	//bool hasEntered = false;
	float teleportTimer = 4f;
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
				chosen = hit.collider.gameObject;
				if (chosen.GetComponent<Rigidbody> () != null) {
					if (OVRInput.Get (OVRInput.Button.SecondaryHandTrigger, OVRInput.Controller.Touch) == true) {
						if (isGrabbed == false) {
							isGrabbed = true;
							chosen.GetComponent<Rigidbody> ().isKinematic = true;
							chosenparent = chosen.transform.parent;
							chosen.transform.parent = transform;
							if (OVRInput.Get (OVRInput.Button.SecondaryThumbstickRight, OVRInput.Controller.Touch) == true) {
								chosen.transform.Rotate (Vector3.right * 3f * Time.deltaTime);
							}
							if (OVRInput.Get (OVRInput.Button.SecondaryThumbstickLeft, OVRInput.Controller.Touch) == true) {
								chosen.transform.Rotate (Vector3.left * 3f * Time.deltaTime);
							}
							//chosen.transform.position = transform.position - transform.forward;
						}
					} else {
						isGrabbed = false;
						chosen.transform.parent = null;
						chosen.GetComponent<Rigidbody> ().isKinematic = false;
						//chosen = null;
					}
				} else if (chosen.GetComponent<TerrainCollider> () != null) {
					if (OVRInput.Get (OVRInput.Button.One, OVRInput.Controller.Touch) == true) {
						teleportTimer -= Time.deltaTime;
						if (teleportTimer < 0) {
							GameObject.Find("OVRPlayerController").transform.position = hit.point;
							teleportTimer = 4f;
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

	void OnTriggerStay(Collider other)
	{
		if (raycast.enabled == false) {
			chosen = other.gameObject;
			if (chosen.GetComponent<Rigidbody> () != null) {
				if (OVRInput.Get (OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) == 1.0f) {
					if (isGrabbed == false) {
						isGrabbed = true;
						chosen.GetComponent<Rigidbody> ().isKinematic = true;
						chosenparent = chosen.transform.parent;
						chosen.transform.parent = transform;
						//chosen.transform.position = transform.position - transform.forward;
					}
				} else {
					isGrabbed = false;
					chosen.transform.parent = null;
					chosen.GetComponent<Rigidbody> ().isKinematic = false;
				}
			}
		}
	}
}
