using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>

[CreateAssetMenu]
public class EscapePlan : ScriptableObject
{
    public string title;
    public string description;
    public Objective[] objectives;   
}

[System.Serializable]
public class Objective
{
    public enum Type { None, Collect, Enter, Talk, Use }
    public Type taskType;
    public string taskTarget;
}
