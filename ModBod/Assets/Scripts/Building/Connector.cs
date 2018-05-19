using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour {
    [SerializeField] private Connector paired;
    [SerializeField] private SpriteRenderer graphic;

    public BodyPart part;

    private Buildable buildable;

	// Use this for initialization
	void Start () {
		if(paired != null)
        {
            Pair(paired);
        }
        part = transform.parent.GetComponent<BodyPart>();
    }

    public void Pair(Connector other)
    {
        // Set Position
        paired = other;
        graphic.enabled = false;
        enabled = false;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (enabled)
        {
            if (buildable == null)
            {
                buildable = transform.parent.GetComponent<Buildable>();
            }

            if (buildable != null && other.tag == "Connector")
            {
                Connector otherConn = other.GetComponent<Connector>();
                if (otherConn.enabled)
                {
                    transform.parent.GetComponent<Buildable>().Snap(this, other.GetComponent<Connector>());
                }
            }
        }
    }

    public void SetBuildMode(bool isOn)
    {
        if (isOn)
        {
            if (!paired)
            {
                enabled = true;
                graphic.enabled = true;
            }
        }
        else
        {
            enabled = false;
            graphic.enabled = false;

        }
    }
}
