using UnityEngine;
using System.Collections;

public class DestroyWithoutKinect : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		if(HelloKinect.UseKinect == false) {
			DestroyImmediate(this.gameObject);
		} else {

			Destroy(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

}
