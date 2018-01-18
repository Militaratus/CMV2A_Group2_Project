using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
