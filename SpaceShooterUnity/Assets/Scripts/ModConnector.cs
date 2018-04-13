using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModConnector : MonoBehaviour {
	public ModPiece parentPiece;

	[SerializeField] private SpriteRenderer spriteRend;

	void OnTriggerEnter2D(Collider2D other)
	{
		Transform hitConnector = other.transform;
		parentPiece.Snap (this, hitConnector);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		parentPiece.UnSnap ();
	}

	public void SetVisible(bool val){
		spriteRend.enabled = val;
	}
}
