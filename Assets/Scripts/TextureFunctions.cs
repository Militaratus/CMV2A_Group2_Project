using UnityEngine;
using System.Collections;

public class TextureFunctions
{
    public static Color TextureFunction0(float x, float y, float v1, float v2, float v3)
    {
        float enlighten = 0;

        if (y == (v1 / 10))
        {
            enlighten = v1 / 10;
        }

        if (y == (v2 / 10))
        {
            enlighten = v2 / 10;
        }

        if (y == (v3 / 10))
        {
            enlighten = v3 / 10;
        }

        int xor = Mathf.RoundToInt(x) == Mathf.RoundToInt(y) ? 0 : 1;
        return enlighten * Color.red;
        //return Mathf.Sin(x / 2) * Color.red;
    }
}
