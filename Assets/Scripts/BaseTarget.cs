using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTarget : MonoBehaviour
{
    // Basics
    public string[] targetTraits;
    bool[] revealedTraits;

	// Use this for initialization
	void Start ()
    {
        PopulateTraits();
    }

    void PopulateTraits()
    {
        targetTraits = new string[6];
        revealedTraits = new bool[6];

        int randomTrait = 0;
        for (int i = 0; i < targetTraits.Length; i++)
        {
            randomTrait = Random.Range(0, 10);

            switch(randomTrait)
            {
                case 0:
                    targetTraits[i] = "Cat"; break;
                case 1:
                    targetTraits[i] = "Belter"; break;
                case 2:
                    targetTraits[i] = "Smoker"; break;
                case 3:
                    targetTraits[i] = "Eyepatcher"; break;
                case 4:
                    targetTraits[i] = "Scar"; break;
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
