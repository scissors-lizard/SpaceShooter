using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCell {
    public BodyPart part;
    public bool[] validConnectors;
    public BodyPart[] connectedParts;
    public int col, row;
    public Vector2 localPos;

    public BuildCell()
    {
        part = null;
        validConnectors = new bool[] { false, false, false, false };
        connectedParts = new BodyPart[] { null, null, null, null };
    }
}
