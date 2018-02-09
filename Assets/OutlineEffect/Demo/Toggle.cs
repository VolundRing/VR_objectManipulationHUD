using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cakeslice
{
    public class Toggle : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
			if(gameObject.transform.parent != null && gameObject.transform.parent != GameObject.Find("RightHandAnchor").transform)
            {
				Outline[] _outline = gameObject.GetComponentsInChildren<Outline> ();
				for (int i = 0; i < _outline.Length; i++) {
					_outline [i].enabled = true;
				}

//				gameObject.GetComponentsinChildren<Outline>().enabled = true;
			}
			if (gameObject.transform.parent == null && gameObject.transform.parent != GameObject.Find("RightHandAnchor").transform){
				Outline[] _outline = gameObject.GetComponentsInChildren<Outline> ();
				for (int i = 0; i < _outline.Length; i++) {
					_outline [i].enabled = false;
				}
			}
        }
    }
}