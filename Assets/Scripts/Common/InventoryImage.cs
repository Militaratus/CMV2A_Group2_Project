using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class InventoryImage : MonoBehaviour
{
	// Use the Editor to spawn an image, on Release use default white.
	public void ShowImage (string name)
    {
#if UNITY_EDITOR
        Object myAsset = Resources.Load("Items/" + name);
        Texture2D myImage = AssetPreview.GetAssetPreview(myAsset);
        GetComponent<RawImage>().texture = myImage;
#endif
        GetComponent<RawImage>().color = Color.white;

    }

    // Hide the image (color)
    public void HideImage()
    {
        GetComponent<RawImage>().color = Color.clear;
    }
}
