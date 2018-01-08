using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameMode { LifeWithoutParole, DeathRow };
    internal GameMode setGameMode;

    // Ensure this is never destroyed
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
