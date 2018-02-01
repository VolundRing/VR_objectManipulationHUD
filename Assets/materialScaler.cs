using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {
	void Start() {
		Texture myTexture = Resources.Load("Wood") as Texture;    
		MeshRenderer thisMesh = gameObject.GetComponent<MeshRenderer> ();
		thisMesh.material.mainTextureScale = new Vector2(transform.lossyScale.x*0.5f,transform.lossyScale.y*0.5f);       

	}
}