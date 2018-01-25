using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class SplashManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        SceneManager.LoadSceneAsync(1);
    }
}