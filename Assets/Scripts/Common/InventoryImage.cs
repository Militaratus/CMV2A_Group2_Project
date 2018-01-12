using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryImage : MonoBehaviour
{
	// Use this for initialization
	public void ShowImage (string name)
    {
        Object myAsset = Resources.Load("Items/" + name);
        Texture2D myImage = AssetPreview.GetAssetPreview(myAsset);
        GetComponent<RawImage>().texture = myImage;
        GetComponent<RawImage>().color = Color.white;

    }

    public void HideImage()
    {
        GetComponent<RawImage>().color = Color.clear;
    }
}
