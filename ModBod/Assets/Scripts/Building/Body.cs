﻿using System.Collections;
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
    private bool[,] integrityGrid;
    public bool needIntegrityCheck = false;

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

    private void Update()
    {
        if (needIntegrityCheck)
        {
            CheckIntegrity();
            needIntegrityCheck = false;
        }
    }

    private void Start()
    {
        SetBuildMode(false);
    }

    public void AddPart(BodyPart p, int col, int row, Dir rot)
    {
        bodyParts.Add(p);
        p.transform.SetParent(transform);
        grid.SetBodyPart(col, row, p, rot);
        RecalculateMass();
    }

    public void RemovePart(BodyPart p)
    {
        bodyParts.Remove(p);
        p.transform.SetParent(null);
        needIntegrityCheck = true;
        RecalculateMass();
    }

    private void CheckIntegrity()
    {
        // Check structural integrity, destroy any isolated segments
        integrityGrid = new bool[maxCols,maxRows];
        CheckCell(maxCols / 2, maxRows / 2);
        BodyPart p;
        for(int i = 0; i < maxCols; i++)
        {
            for(int j = 0; j < maxRows; j++)
            {
                if(integrityGrid[i,j] == false)
                {
                    p = grid.GetPartAt(i, j);
                    if(p != null)
                    {
                        p.Kill();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Recursive flood fill to check what cells are attached to the main body. 
    /// </summary>
    /// <param name="col"></param>
    /// <param name="row"></param>
    private void CheckCell(int col, int row)
    {
        if (col > 0 && col < maxCols && row > 0 && row < maxRows)
        {
            if (!integrityGrid[col, row] && (grid.GetCellAt(col, row).part != null))
            {
                integrityGrid[col, row] = true;
                CheckCell(col + 1, row);
                CheckCell(col, row + 1);
                CheckCell(col - 1, row);
                CheckCell(col, row - 1);
            }
        }
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

    public void UpdateHighlights()
    {
        DestroyHighlights();
        CreateHighlights();
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
        return grid.GetCellAt(gridX, gridY);
    }

    public bool IsMouseOverValidSlot(Vector3 pos)
    {
        BuildCell c = GetCellAtPos(pos);
        if(c == null)
        {
            return false;
        }
        return grid.CheckValidBuildPos(c.col, c.row);
    }

    public bool CheckValidPlacement(BuildCell cell, BodyPart part, Dir facing)
    {
        return cell.part == null;
    }
}
