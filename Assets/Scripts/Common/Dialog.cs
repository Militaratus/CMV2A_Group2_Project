using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>

[CreateAssetMenu]
public class Dialog : ScriptableObject
{
    public Dialogue[] dialogue;
}

[System.Serializable]
public class Dialogue
{
    public string responseText;
    public bool completesObjective;
    public string choice1Text;
    public int choice1Path;
    public string choice2Text;
    public int choice2Path;
}
