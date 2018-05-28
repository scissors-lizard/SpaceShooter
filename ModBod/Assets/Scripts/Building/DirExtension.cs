using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirExtension {

    public static Dir CW(this Dir d)
    {
        switch (d)
        {
            case Dir.N:
                return Dir.E;

            case Dir.E:
                return Dir.S;

            case Dir.S:
                return Dir.W;

            default:
                return Dir.N;
        }
    }

    public static Dir CCW(this Dir d)
    {
        switch (d)
        {
            case Dir.N:
                return Dir.W;

            case Dir.E:
                return Dir.N;


            case Dir.S:
                return Dir.E;

            default:
                return Dir.S;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="d">first dir</param>
    /// <param name="other">second dir</param>
    /// <returns>How many clockwise 90-degree steps there between the two directions (ex. N and S will return 2, since they are 180 degreees apart.</returns>
    public static int RotSteps(this Dir d, Dir other)
    {
        if(other == d)
        {
            return 0;
        }
        switch (d)
        {
            case Dir.N:
                if (other == Dir.E) {
                    return 1;
                }
                else if (other == Dir.S)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }

            case Dir.E:
                if (other == Dir.S)
                {
                    return 1;
                }
                else if (other == Dir.W)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }

            case Dir.S:
                if (other == Dir.W)
                {
                    return 1;
                }
                else if (other == Dir.N)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }

            default: // W
                if (other == Dir.N)
                {
                    return 1;
                }
                else if (other == Dir.E)
                {
                    return 2;
                }
                else 
                {
                    return 3;
                }
        }
    }

    public static Vector2 ToVector2(this Dir d)
    {
        switch (d)
        {
            case Dir.N:
                return new Vector2(0f,1f);

            case Dir.E:
                return new Vector2(1f, 0f);

            case Dir.S:
                return new Vector2(0f, -1f);

            default:
                return new Vector2(-1f, 0f);

        }
    }
}
