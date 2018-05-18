using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour {
    [SerializeField] private Connector paired;
    [SerializeField] private SpriteRenderer graphic;

    private BuildPiece buildPiece;

	// Use this for initialization
	void Start () {
		if(paired != null)
        {
            Pair(paired);
        }
        buildPiece = transform.parent.GetComponent<BuildPiece>();
	}

    public void Pair(Connector other)
    {
        // Set Position
        paired = other;
        graphic.enabled = false;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (enabled && other.tag == "Connector")
        {
            buildPiece.OnConnectorCollision(this, other);
        }
    }
}
