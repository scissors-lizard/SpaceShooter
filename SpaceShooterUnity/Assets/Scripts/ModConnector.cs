using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModConnector : MonoBehaviour {
	public ModPiece parentPiece;

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("trig");
		Transform hitConnector = other.transform;
		parentPiece.Snap (this, hitConnector);
	}
}
