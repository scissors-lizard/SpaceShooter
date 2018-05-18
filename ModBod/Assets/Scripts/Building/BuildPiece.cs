using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BodyPart))]
public class BuildPiece : MonoBehaviour {
    private Camera cam;
    private Connector[] connectors;
    private bool snapping = false;
    private GameObject colliderHolder;
    private Vector2 lastSnapPos;
    private BodyPart bodyPart;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        bodyPart = GetComponent<BodyPart>();
        bodyPart.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!snapping)
        {
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = transform.position.z;
            transform.position = pos;
            transform.Rotate(0f, 0f, Input.GetAxis("Scroll") * Time.deltaTime * 5000f); 
        }
        else // snapping
        {
            if(Vector2.Distance(cam.ScreenToWorldPoint(Input.mousePosition), lastSnapPos) > 0.75f)
            {
                snapping = false;
            }

        }
	}

    public void OnConnectorCollision(Connector myConnector, Collider2D otherConnector)
    {
        if (!snapping)
        {
            snapping = true;

            Vector2 targetDir = -otherConnector.transform.up;
            Vector2 forward = myConnector.transform.up;

            float angle = Vector2.SignedAngle(forward, targetDir);
            transform.Rotate(0f, 0f, angle);
            Vector3 offset = otherConnector.transform.position - myConnector.transform.position;
            transform.position += offset;

            lastSnapPos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
