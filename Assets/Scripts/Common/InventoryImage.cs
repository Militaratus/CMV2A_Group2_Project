using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

public class InventoryImage : MonoBehaviour
{
	// Use this for initialization
	public void ShowImage (string name)
    {
#if UNITY_EDITOR
        Object myAsset = Resources.Load("Items/" + name);
        Texture2D myImage = AssetPreview.GetAssetPreview(myAsset);
        GetComponent<RawImage>().texture = myImage;
#endif
        GetComponent<RawImage>().color = Color.white;

    }

    public void HideImage()
    {
        GetComponent<RawImage>().color = Color.clear;
    }
}
