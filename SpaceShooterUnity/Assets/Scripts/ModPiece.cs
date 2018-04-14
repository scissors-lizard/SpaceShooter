using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModPiece : MonoBehaviour {
	[SerializeField] private GameObject ghost;
	[SerializeField] private GameObject[] visuals;

	private Transform hitConnector;
	private ModConnector[] connectors;
	private bool snapped = false;
	private Vector3 ghostPos;
	private Quaternion ghostRot;

	void Awake (){
		connectors = gameObject.GetComponentsInChildren<ModConnector> ();
		for (int i = 0; i < connectors.Length; i++) {
			connectors[i].parentPiece = this;
		}
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = new Vector3 (pos.x, pos.y, transform.position.z);

		if (snapped) {
			ghost.transform.position = ghostPos;
			ghost.transform.rotation = ghostRot;
			if (Input.GetMouseButtonDown (0)) {
				Attach ();
			}
		}
	}

	void Attach(){
		transform.position = ghostPos;
		transform.rotation = ghostRot;
		ghost.SetActive (false);
		this.enabled = false;

		for (int i = 0; i < visuals.Length; i++) {
			visuals [i].SetActive (true);
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
		// Reset ghost
		ghost.SetActive (true);
		ghost.transform.position = transform.position;
		ghost.transform.rotation = transform.rotation;
//
//		this.hitConnector = hitConnector;
//		float angle = Quaternion.Angle (childConnector.transform.rotation, hitConnector.transform.rotation);
//		transform.rotation = transform.rotation * Quaternion.Euler(0f, angle + 180f, 0f);
//
//		Vector3 offset = hitConnector.transform.position - childConnector.transform.position;
//		transform.position += offset;
//		snapped = true;
//

		this.hitConnector = hitConnector;
		float angle = Quaternion.Angle (childConnector.transform.rotation, hitConnector.transform.rotation);
		ghost.transform.RotateAround(childConnector.transform.localPosition + ghost.transform.position, -Vector3.forward, angle + 180f);

		Vector3 offset = hitConnector.transform.position - childConnector.transform.position;
		ghost.transform.position += offset;

		ghostPos = ghost.transform.position;
		ghostRot = ghost.transform.rotation;
		snapped = true;

		for (int i = 0; i < visuals.Length; i++) {
			visuals [i].SetActive (false);
		}
		for (int i = 0; i < connectors.Length; i++) {
			connectors [i].SetVisible (false);
		}
	}

	public void UnSnap(){
		// Reset ghost
		ghost.gameObject.SetActive (false);

		snapped = false;

		for (int i = 0; i < visuals.Length; i++) {
			visuals [i].SetActive (true);
		}

		for (int i = 0; i < connectors.Length; i++) {
			connectors [i].SetVisible (true);
		}
	}
}
