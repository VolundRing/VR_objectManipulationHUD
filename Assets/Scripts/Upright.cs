﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upright : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3 (-90f, transform.eulerAngles.y, transform.eulerAngles.z);
	}
}
