using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyGrid {
    private int cols, rows; // How much memory to allocate

    private BodyPart[,] structure;

    public BodyGrid(int cols,int rows)
    {
        this.cols = cols;
        this.rows = rows;
        structure = new BodyPart[cols, rows];
    }

    public BodyPart GetPartAt(int x, int y)
    {
        int convX = x + cols / 2;
        int convY = y + rows / 2;
        if(convX < 0 || convY < 0 || convX > cols -1 || convY > rows - 1)
        {
            return null;
        }
        else
        {
            return structure[convX, convY];
        }
    }

    public void SetBodyPart(int x, int y, BodyPart part)
    {
        if (x < 0 || y < 0 || x > cols - 1 || y > rows - 1)
        {
            return;
        }
        else
        {
            structure[x, y] = part;
        }
    }

}
