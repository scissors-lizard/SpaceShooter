using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {
    public float mass;
    public List<BodyPart> bodyParts;

    [SerializeField] private int maxCols, maxRows;
    [SerializeField] private float cellSize;
    [SerializeField] private BodyPart core;
    [SerializeField] private GameObject buildSlotHighlightPrefab;
    [SerializeField] private BodyGrid grid;
    private List<GameObject> highlights;

    // Use this for initialization
    void Awake () {
        bodyParts = new List<BodyPart>(GetComponentsInChildren<BodyPart>());
        for(int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].body = this;
        }
        grid.Initialize(maxCols, maxRows, cellSize);

        int centerCol = maxCols / 2, centerRow = maxRows / 2;

        grid.SetBodyPart(centerCol, centerCol, core);
        grid.SetValidConnectors(centerCol, centerCol, new bool[] { false, false, true, true});

        grid.SetBodyPart(centerCol+1, centerRow + 1, core);
        grid.SetValidConnectors(centerCol + 1, centerRow + 1, new bool[] {false, true, false, false});

        grid.SetBodyPart(centerCol, centerRow + 1, core);
        grid.SetValidConnectors(centerCol, centerRow + 1, new bool[] { false, false, false, true});

        grid.SetBodyPart(centerCol + 1, centerRow, core);
        grid.SetValidConnectors(centerCol + 1, centerRow, new bool[] {false, true, true, false});
        highlights = new List<GameObject>();
        RecalculateMass();
    }

    private void Start()
    {
        SetBuildMode(false);
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
        if (isOn)
        {
            CreateHighlights();
        }
        else
        {
            DestroyHighlights();
        }
    }

    private void CreateHighlights()
    {
        for(int i = 0; i < maxCols; i++)
        {
            for (int j = 0; j < maxRows; j++)
            {

                if(grid.CheckValidBuildPos(i, j)){
                    GameObject highlight = Instantiate(buildSlotHighlightPrefab) as GameObject;
                    highlight.transform.SetParent(transform);
                    highlight.transform.localRotation = Quaternion.identity;
                    highlight.transform.localPosition = new Vector3((i - maxCols / 2) * cellSize - cellSize * 0.5f, (j - maxRows / 2) * cellSize - cellSize * 0.5f, transform.position.z);
                    highlights.Add(highlight);
                }
            }
        }
    }

    private void DestroyHighlights()
    {
        for(int i = highlights.Count-1;i>=0; i--)
        {
            Destroy(highlights[i]);
        }
    }

    public BuildCell GetCellAtPos(Vector3 pos)
    {
        Vector3 localPos = transform.InverseTransformPoint(pos);
        int gridX = Mathf.CeilToInt(localPos.x / cellSize) + maxCols/2;
        int gridY = Mathf.CeilToInt(localPos.y / cellSize) + maxRows/2;
        BuildCell c = grid.GetCellAt(gridX, gridY);
        Debug.Log("NAWL? "+(c == null)); 
        return grid.GetCellAt(gridX, gridY);
    }
}
