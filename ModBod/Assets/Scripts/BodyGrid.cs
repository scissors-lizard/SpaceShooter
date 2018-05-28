using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyGrid : MonoBehaviour{
    private int cols, rows; // How much memory to allocate
    private GridCell[,] grid;
    private float cellSize;

    public BodyGrid(int cols,int rows, float cellSize)
    {
        this.cols = cols;
        this.rows = rows;
        this.cellSize = cellSize;

        grid = new GridCell[cols, rows];

        for(int i = 0; i < cols; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                grid[i,j] = new GridCell();
            }
        }
    }

    public BodyPart GetPartAt(int x, int y)
    {
        if(x>0 && x < cols && y>0 && y<rows)
        {
            return grid[x,y].part;
        }
        else
        {
            return null;
        }
    }

    public void SetBodyPart(int x, int y, BodyPart part, Dir facing = Dir.N)
    {
        if (x < 0 || y < 0 || x > cols - 1 || y > rows - 1)
        {
            return;
        }
        else
        {
            grid[x, y].part = part;
            if (part.validConnectors != null)
            {
                grid[x, y].validConnectors = RotateVals(part.validConnectors, Dir.N.RotSteps(facing));
            }
        }
    }

    public void SetValidConnectors(int x, int y, bool[] vals, Dir facing = Dir.N)
    {
        grid[x, y].validConnectors = RotateVals(vals, Dir.N.RotSteps(facing));
    }


    public bool[] GetOpenConnectors(int x, int y)
    {
        bool[] retVal = new bool[4];
        for(int i = 0; i < 4; i++)
        {
            if(grid[x,y].validConnectors[i] && grid[x, y].connectedParts[i] == null)
            {
                retVal[i] = true;
            }
            else
            {
                retVal[i] = false;
            }
        }
        return retVal;
    }

    /// <summary>
    /// Shifts all values in the array, wrapping in both directions. 
    /// </summary>
    /// <param name="vals"> input array</param>
    /// <param name="steps">number of indices up or down to shift</param>
    /// <returns></returns>
    private bool[] RotateVals(bool[] vals, int steps)
    {
        if(vals == null || vals.Length == 0)
        {
            return null;
        }

        bool[] retVal = new bool[vals.Length];
        for(int i = 0; i < vals.Length; i++)
        {
            int outIndex = i + steps;
            while(outIndex > vals.Length - 1)
            {
                outIndex -= vals.Length;
            }
            while(outIndex < 0)
            {
                outIndex += vals.Length;
            }
            retVal[outIndex] = vals[i];
        }
        return retVal;
    }

    public bool CheckValidBuildPos(int col, int row)
    {
        if(col < 0 || col > cols - 1 || row < 0 || row > rows - 1)
        {
            Debug.Log("Checked build position out of bounds @ (" + col + "," + row + ")");
            return false;
        }

        int c = col;
        int r = row-1;
        int adjacentParts = 0;
        if (r >= 0 && grid[c,r].part != null )
        {
            adjacentParts ++; 
            if (!grid[c, r].validConnectors[0])
            {
                return false;
            }
        }
        c = col-1;
        r = row;
        if (c >= 0 && grid[c, r].part != null)
        {
            adjacentParts++;
            if (!grid[c, r].validConnectors[1])
            {
                return false;
            }
        }
        c = col;
        r = row+1;
        if (r <= rows -1 && grid[c, r].part != null)
        {
            adjacentParts++;
            if (!grid[c, r].validConnectors[2])
            {
                return false;
            }
        }
        c = col+1;
        r = row;
        if (c <= cols -1 && grid[c, r].part != null)
        {
            adjacentParts++;
            if (!grid[c, r].validConnectors[3])
            {
                return false;
            }
        }

        // All directions check out, valid to build if there's anything adjacent to attach on to.
        return adjacentParts > 0;
    }

    


    private class GridCell
    {
        public BodyPart part;
        public bool[] validConnectors;
        public BodyPart[] connectedParts;

        public GridCell()
        {
            part = null;
            validConnectors = new bool[] { false, false, false, false };
            connectedParts = new BodyPart[] {null, null, null, null};
        }
    }
}
