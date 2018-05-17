using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		Vector3 lookTarget = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		lookTarget.z = transform.position.z;
		transform.up = lookTarget - transform.position;
	}
}
