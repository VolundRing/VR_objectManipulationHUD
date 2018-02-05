using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

	public bool spwnMode = false ;
	public static GameObject[] deckObjs;
	public int whichDeckObj = 0;
	public int PFarrayLength = 6;
	private bool newRPress = false;
	private bool newLPress = false;
	private bool newObj = false;
	GameObject miniObj;

	void Start () {
		
	}

	// Mode indicator
	bool modeSwitch () {
			return spwnMode;
	}

	int whichObj () {
		return whichDeckObj;
	}
		
	void Update () {
		//Mode switching
		if (spwnMode == false) {
			if (OVRInput.GetDown (OVRInput.Button.PrimaryThumbstick) == true) {
				spwnMode = true ;
			}
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
					if (whichDeckObj < PFarrayLength) { 
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
				newObj = true;
			}

			miniObj.transform.parent = transform;
		}
	}
	void MinInstan () {
		if (spwnMode == true) {

		}
	}
}
