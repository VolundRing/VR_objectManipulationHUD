using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandAction : MonoBehaviour {
	public LineRenderer raycast;
	public GameObject newraychosen;
	GameObject oldraychosen;
	GameObject chosen2;
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
				newraychosen = hit.collider.gameObject;
				if (newraychosen != oldraychosen) {
					if (oldraychosen != null && oldraychosen.GetComponent<Rigidbody> () != null) {
						oldraychosen.transform.parent = null;
						oldraychosen.GetComponent<Rigidbody> ().isKinematic = false;
						oldraychosen = null;

						//isGrabbed = false;
					}
					oldraychosen = newraychosen;
				}
				if (newraychosen.GetComponent<Rigidbody> () != null) {
					if (OVRInput.Get (OVRInput.Button.SecondaryHandTrigger, OVRInput.Controller.Touch) == true) {
						if (isGrabbed == false) {
							isGrabbed = true;
							newraychosen.GetComponent<Rigidbody> ().isKinematic = true;
							chosenparent = newraychosen.transform.parent;
							newraychosen.transform.parent = transform;

							//chosen.transform.position = transform.position - transform.forward;
						} else {
							if (OVRInput.Get (OVRInput.Button.SecondaryThumbstickRight, OVRInput.Controller.Touch) == true) {
								newraychosen.transform.Rotate (Vector3.right * 3f * Time.deltaTime);
							}
							if (OVRInput.Get (OVRInput.Button.SecondaryThumbstickLeft) == true) {
								newraychosen.transform.Rotate (Vector3.left * 3f * Time.deltaTime);
							}
						}
					} else {
						isGrabbed = false;
						newraychosen.transform.parent = null;
						newraychosen.GetComponent<Rigidbody> ().isKinematic = false;
						newraychosen = null;
					}
				} else if (newraychosen.GetComponent<TerrainCollider> () != null) {
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

			if (newraychosen != null && newraychosen.GetComponent<Rigidbody> () != null) {
				newraychosen.transform.parent = null;
				newraychosen.GetComponent<Rigidbody> ().isKinematic = false;
				newraychosen = null;
				//isGrabbed = false;
			}


		}
	}

	void OnTriggerStay(Collider other)
	{
		if (OVRInput.Get (OVRInput.Touch.SecondaryIndexTrigger, OVRInput.Controller.Touch) == true) {
			if (newraychosen != null &&newraychosen.GetComponent<Rigidbody> () != null) {
				newraychosen.transform.parent = null;
				newraychosen.GetComponent<Rigidbody> ().isKinematic = false;
				newraychosen = null;
			}
			chosen2 = other.gameObject;
			if (chosen2.GetComponent<Rigidbody> () != null) {
				if (OVRInput.Get (OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) == 1.0f) {

					if (isGrabbed == false) {
						isGrabbed = true;
						chosen2.GetComponent<Rigidbody> ().isKinematic = true;
						chosenparent = chosen2.transform.parent;
						chosen2.transform.parent = transform;
						//chosen.transform.position = transform.position;
						//chosen.transform.position = transform.position - transform.forward;
					}
				} else {
					isGrabbed = false;
					chosen2.transform.parent = null;
					chosen2.GetComponent<Rigidbody> ().isKinematic = false;
					chosen2 = null;
				}
			}
		}
		
	}
}
