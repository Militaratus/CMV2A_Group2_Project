using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTarget : MonoBehaviour
{
    // Basics
    string[] targetTraits;
    public bool revealed = false;

	// Use this for initialization
	void Start ()
    {
		
	}

    void PopulateTraits()
    {
        targetTraits = new string[4];

        int randomTrait = 0;
        for (int i = 0; i < targetTraits.Length; i++)
        {
            randomTrait = Random.Range(0, 10);

            switch(randomTrait)
            {
                case 0:
                    targetTraits[i] = "Drunk"; break;
                case 1:
                    targetTraits[i] = "Drunk"; break;
                case 2:
                    targetTraits[i] = "Drunk"; break;
                case 3:
                    targetTraits[i] = "Drunk"; break;
                case 4:
                    targetTraits[i] = "Drunk"; break;
                case 5:
                    targetTraits[i] = "Drunk"; break;
                case 6:
                    targetTraits[i] = "Drunk"; break;
                case 7:
                    targetTraits[i] = "Drunk"; break;
                case 8:
                    targetTraits[i] = "Drunk"; break;
                case 9:
                    targetTraits[i] = "Drunk"; break;
                default:
                    targetTraits[i] = "Error"; break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
