﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upright : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody> ().freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (GetComponent<Rigidbody> ().freezeRotation == false) {
			this.GetComponent<Rigidbody> ().freezeRotation = true;
		}*/
		transform.rotation = Quaternion.Euler (-90f, transform.eulerAngles.y, transform.eulerAngles.z);
		if (transform.position.y < 0) {
			transform.position = new Vector3 (transform.position.x, 1f, transform.position.z);
		}

	}
}
