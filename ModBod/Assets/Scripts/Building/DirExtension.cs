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
