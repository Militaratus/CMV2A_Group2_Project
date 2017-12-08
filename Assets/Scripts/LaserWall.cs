using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LaserWall : MonoBehaviour
{
    private const int Width = 64;
    private const int Height = 64;

    public int SelectedMethod;
    public float Variable1;
    public float Variable2;
    public float Variable3;

    private Texture2D Texture = null;
    private delegate Color TextureFunctionDelegate(float x, float y, float variable1, float variable2, float variable3);

    // Use this for initialization
    void Start ()
    {
        // create texture
        Texture = new Texture2D(Width, Height);
        Texture.filterMode = FilterMode.Bilinear;

        // set the texture of the current material
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sharedMaterial.mainTexture = Texture;
        }
    }

    void OnDestroy()
    {
        DestroyImmediate(Texture);
    }

    TextureFunctionDelegate GetDelegate(int selectedMethod)
    {
        switch (selectedMethod)
        {
            case 0:
                return TextureFunctions.TextureFunction0;
            default:
                return TextureFunctions.TextureFunction0;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        // get delegate function
        TextureFunctionDelegate function = GetDelegate(SelectedMethod);

        Variable1 = UnityEngine.Random.Range(0, 10);
        Variable2 = UnityEngine.Random.Range(0, 10);
        Variable3 = UnityEngine.Random.Range(0, 10);

        // render the image using the selected function
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Texture.SetPixel(x, y, function(x / (float)Width, y / (float)Height, Variable1, Variable2, Variable3));
            }
        }

        // push the texture to the GPU
        Texture.Apply();
    }
}
