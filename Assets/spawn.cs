using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

	public LineRenderer raycast;
	public bool spwnMode = false ;
	public static GameObject[] deckObjs;
	public int whichDeckObj = 0;
	public GameObject chosen;
	public int PFarrayLength = 6;
	private bool newRPress = false;
	private bool newLPress = false;
	private bool newObj = false;
	public int groupLength = 0;
	GameObject miniObj;
	GameObject fullObj;
	public List<GameObject> Group = new List<GameObject>();


	void Start () {
		
	}
		
		
	void Update () {
		
		//Mode switching
		if (spwnMode == false) {
			RaycastHit hit;
			Vector3 fwd = transform.TransformDirection (Vector3.forward);
			Vector3 endPosition = transform.position;
			if (Physics.Raycast (transform.position, fwd, out hit, 10000)) {
				endPosition = hit.point;
				chosen = hit.collider.gameObject;
				if (chosen.GetComponent<Rigidbody> () != null) {
					if (OVRInput.GetDown(OVRInput.Button.Three, OVRInput.Controller.Touch) == true) {
						if (Group.Contains (chosen)) { 
							Group.Remove (chosen);
							groupLength = Group.Count;
						} else {
							Group.Add (chosen); 
							groupLength = Group.Count;
						}
					}
				}
			}
				

			if (OVRInput.GetDown (OVRInput.Button.PrimaryThumbstick) == true) {
				spwnMode = true ;
			}
			raycast.SetPosition (0, transform.position);
			raycast.SetPosition (1, endPosition);
		}
		else {
			if (OVRInput.GetDown (OVRInput.Button.PrimaryThumbstick) == true) {
				spwnMode = false ;
			}		
		}
		if (spwnMode == true) {
			deckObjs = Resources.LoadAll<GameObject> ("Prefabs");
			//whichDeckObj incrementor
			if (newRPress == false) {				
				if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickRight) == true) {
					if (whichDeckObj < deckObjs.Length) { 
						whichDeckObj++;					
					}
					newRPress = true;
				}
			}
			if (newRPress == true) {
				if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickRight) == false) {
					newRPress = false;
				}
			}
			if (newLPress == false) {				
				if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickLeft) == true) {
					if (whichDeckObj > 0) {
						whichDeckObj--;	
					}
					newLPress = true;
				}
			}
			if (newLPress == true) {
				if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickLeft) == false) {
					newLPress = false;
				}
			}
			//Instantiate miniature
			if (newLPress == true || newRPress == true){
				newObj = false;
			}
			if (newObj == false){
				Destroy (miniObj);
				miniObj = Instantiate (deckObjs [whichDeckObj], transform.position, transform.rotation) as GameObject;
				miniObj.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
				miniObj.GetComponent<Rigidbody> ().isKinematic = true;
				miniObj.transform.parent = transform;

			}
			newObj = true;

			if (OVRInput.GetDown (OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch) == true) {
				//Destroy (miniObj);
				fullObj = Instantiate (deckObjs [whichDeckObj], new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.2f), transform.rotation) as GameObject;
				fullObj.GetComponent<Rigidbody> ().isKinematic = true;
				fullObj.transform.parent = transform;
				fullObj.AddComponent<Upright> ();


			}
			if (OVRInput.GetUp (OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch) == true) {
				fullObj.GetComponent<Rigidbody> ().isKinematic = false;
				fullObj.transform.parent = null;
			}


		}
	}
}

