using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {
    public float mass;
    public List<BodyPart> bodyParts;

	// Use this for initialization
	void Awake () {
        bodyParts = new List<BodyPart>(GetComponentsInChildren<BodyPart>());
        for(int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].body = this;
        }
        RecalculateMass();
    }
	
	public void AddPart(BodyPart p)
    {
        bodyParts.Add(p);
        p.transform.SetParent(transform);
        RecalculateMass();
    }

    public void RemovePart(BodyPart p)
    {
        bodyParts.Remove(p);
        p.transform.SetParent(null);

        RecalculateMass();
    }

    private void RecalculateMass()
    {
        float sumMass = 0f;
        for (int i = 0; i < bodyParts.Count; i++)
        {
            sumMass += bodyParts[i].mass;
        }
        mass = sumMass;
    }

    public void SetBuildMode(bool isOn)
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].SetBuildMode(isOn);
        }
    }
}
