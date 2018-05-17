using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		Vector3 lookTarget = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		lookTarget.z = transform.position.z;
		transform.up = lookTarget - transform.position;

		transform.position += Vector3.ClampMagnitude((new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0)),1f) * Time.deltaTime * 3f;
	}
}
