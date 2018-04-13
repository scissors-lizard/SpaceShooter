using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModPiece : MonoBehaviour {
	private Transform hitConnector;
	private ModConnector[] connectors;
	private bool snapped = false;

	void Awake (){
		connectors = gameObject.GetComponentsInChildren<ModConnector> ();
		for (int i = 0; i < connectors.Length; i++) {
			connectors[i].parentPiece = this;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!snapped) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
		}
	}



//	void OnTriggerStay2D(Collider2D other)
//	{
//		otherHit = other;
//	}
//	void OnTriggerExit2D(Collider2D other)
//	{
//		otherHit = null;
//	}
//

	public void Snap(ModConnector childConnector, Transform hitConnector){
		this.hitConnector = hitConnector;
		float angle = Quaternion.Angle (childConnector.transform.rotation, hitConnector.transform.rotation);
		transform.rotation = transform.rotation * Quaternion.Euler(0f, angle + 180f, 0f);

		Vector3 offset = hitConnector.transform.position - childConnector.transform.position;
		transform.position += offset;
		snapped = true;
	}
}
