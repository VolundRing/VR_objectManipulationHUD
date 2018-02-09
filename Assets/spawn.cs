using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawn : MonoBehaviour {

	public LineRenderer  raycast;
	public bool spwnMode = false ;
	public static GameObject[] deckObjs;
	public int whichDeckObj = 0;
	public GameObject chosen;
	public int PFarrayLength = 6;
	private bool newRPress = false;
	private bool newLPress = false;
	private bool newObj = false;
	private int distCount = 0;
	private Vector3 firstDist;
	private Vector3 secondDist;
	public float totalDist = 0;
	public int groupLength = 0;
	GameObject miniObj;
	GameObject fullObj;
	GameObject copyObj;
	GameObject gCopyEmpty;
	public List<GameObject> Group = new List<GameObject>();
	//public GameObject GroupEmpty;
	public GameObject GEmpty;
	public GameObject chosenParent;

	void Update ()
	{
		
		//Group and Copy mode
		if (spwnMode == false) {
			GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = true;
			raycast.enabled = true;
			Destroy (miniObj);
			RaycastHit hit;
			Vector3 fwd = transform.TransformDirection (Vector3.forward);
			Vector3 endPosition = transform.position;
			if (Physics.Raycast (transform.position, fwd, out hit, 10000)) {
				endPosition = hit.point;
				chosen = hit.collider.gameObject;
				if (chosen.GetComponent<Rigidbody> () != null) {
					//Creates copy And parents To hand
					if (OVRInput.GetDown (OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch) == true) {
						//If copy target is part of a group
						if (chosen.transform.parent != null && chosen.transform.parent != transform) {
							chosenParent = chosen.transform.parent.gameObject;
							gCopyEmpty = Instantiate (chosenParent, new Vector3 (transform.position.x, transform.position.y + 0.4f, transform.position.z + 0.7f), transform.rotation) as GameObject;
							//change children to: is kinematic
							Rigidbody[] _rigidbody = gCopyEmpty.GetComponentsInChildren<Rigidbody> ();
							for (int i = 0; i < _rigidbody.Length; i++) {
								_rigidbody [i].isKinematic = true;
							}
							gCopyEmpty.transform.parent = transform;

						}
						//If copy target is not part of group
						else if (chosen.transform.root == chosen.transform) {							
							copyObj = Instantiate (chosen, new Vector3 (transform.position.x, transform.position.y + 0.4f, transform.position.z + 0.2f), transform.rotation) as GameObject;
							copyObj.GetComponent<Rigidbody> ().isKinematic = true;
							copyObj.transform.parent = transform;
							copyObj.AddComponent<Upright> ();

						}
					} else if (OVRInput.GetUp (OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch) == true) {
						//release group
						if (gCopyEmpty != null) {
							gCopyEmpty.transform.parent = null;
							//turn off children's is kinematic
							Rigidbody[] _rigidbody = gCopyEmpty.GetComponentsInChildren<Rigidbody> ();
							for (int i = 0; i < _rigidbody.Length; i++) {
								_rigidbody [i].isKinematic = false;
							}
							gCopyEmpty = null;
						
						} else {
							copyObj.GetComponent<Rigidbody> ().isKinematic = false;
							copyObj.transform.parent = null;
						}
//						if (gCopyEmpty.transform.parent != gCopyEmpty.transform) { 
//							if (OVRInput.GetUp (OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch) == true) {
//								gCopyEmpty.transform.parent = gCopyEmpty.transform;
//								//turn off children's is kinematic
//								Rigidbody[] _rigidbody = gCopyEmpty.GetComponentsInChildren<Rigidbody> ();
//								for (int i = 0; i < _rigidbody.Length; i++) {
//									_rigidbody [i].isKinematic = false;
//								}
//							}
//						}
//						//release copy
//						if (OVRInput.GetUp (OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch) == true) {
//							copyObj.GetComponent<Rigidbody> ().isKinematic = false;
//							copyObj.transform.parent = null;
//						}
					}
				}


				//Grouping
				if (OVRInput.GetDown (OVRInput.Button.Three, OVRInput.Controller.Touch) == true) {
					//Parents to GEmpty
					if (chosen.transform.root != chosen.transform && chosen.GetComponent<Rigidbody> () != null) {
						chosen.transform.parent = null;
						Group.Remove (chosen);
						if (Group.Count != 0) {
							foreach (GameObject o in Group){
								o.transform.parent = null;
							}
							GEmpty.transform.position = Group [0].transform.position;
							foreach (GameObject p in Group){
								p.transform.parent = GEmpty.transform;
							}
						}
					} else if (chosen.transform.root == chosen.transform && chosen.GetComponent<Rigidbody> () != null) {
						if (GEmpty.transform.childCount == 0) {
							GEmpty.transform.position = chosen.transform.position;

						}
						chosen.transform.parent = GEmpty.transform;
						Group.Add (chosen);
					}
				}
				if (OVRInput.GetDown (OVRInput.Button.Four, OVRInput.Controller.Touch) == true) {
					GameObject _distance = GameObject.Find ("Distance");
					GameObject _point1 = GameObject.Find ("Point1");
					GameObject _point2 = GameObject.Find ("Point2");
					if (distCount == 0) {
						_point1.GetComponent<Text> ().enabled = true;
						firstDist = hit.point;
						distCount++;
					} else if (distCount == 1) {
						_point2.GetComponent<Text> ().enabled = true;
						secondDist = hit.point;
						totalDist = Vector3.Distance (firstDist, secondDist);
						_distance.GetComponent<Text> ().text = "" + totalDist + "m";
						distCount++;
					} else if (distCount == 2) {
						_point1.GetComponent<Text> ().enabled = false;
						_point2.GetComponent<Text> ().enabled = false;
						totalDist = 0;
						_distance.GetComponent<Text> ().text = "" + totalDist + "m";
						distCount = 0;
					}
					
				}

				
				//switch to spawn mode
				if (OVRInput.GetDown (OVRInput.Button.PrimaryThumbstick) == true) {
					spwnMode = true;
				}
				raycast.SetPosition (0, transform.position);
				raycast.SetPosition (1, endPosition);
			}
		}else {
				if (OVRInput.GetDown (OVRInput.Button.PrimaryThumbstick) == true) {
					spwnMode = false;
				}		
			}
			//Spawn Mode Code
			if (spwnMode == true) {
				GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = false;
				raycast.enabled = false;
				newObj = false;
				deckObjs = Resources.LoadAll<GameObject> ("Prefabs");
				//whichDeckObj incrementor
				if (newRPress == false) {				
					if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickRight) == true) {
						if (whichDeckObj < deckObjs.Length - 1) { 
							whichDeckObj++;					
						}
						newRPress = true;
					}
				}
				//button Checker
				if (newRPress == true) {
					if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickRight) == false) {
						newRPress = false;
					}
				}
				//whichDeckObj decrementor
				if (newLPress == false) {				
					if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickLeft) == true) {
						if (whichDeckObj > 0) {
							whichDeckObj--;	
						}
						newLPress = true;
					}
				}
				//button Checker
				if (newLPress == true) {
					if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickLeft) == false) {
						newLPress = false;
					}
				}
				//Instantiate miniature
				if (newLPress == true || newRPress == true) {
					newObj = false;
				}
				if (newObj == false) {
					Destroy (miniObj);
					miniObj = Instantiate (deckObjs [whichDeckObj], transform.position, transform.rotation) as GameObject;
					miniObj.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
					miniObj.GetComponent<Rigidbody> ().isKinematic = true;
					miniObj.transform.parent = transform;

				}
				newObj = true;
				//Spawn Object
				if (OVRInput.GetDown (OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch) == true) {
					//Destroy (miniObj);
					fullObj = Instantiate (deckObjs [whichDeckObj], new Vector3 (transform.position.x, transform.position.y + 0.4f, transform.position.z + 0.2f), transform.rotation) as GameObject;
					fullObj.GetComponent<Rigidbody> ().isKinematic = true;
					fullObj.transform.parent = transform;
					fullObj.AddComponent<Upright> ();


				}
				//Release Spwaned object from hand
				if (OVRInput.GetUp (OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch) == true) {
					fullObj.GetComponent<Rigidbody> ().isKinematic = false;
					fullObj.transform.parent = null;
				}


			}
	}
}

