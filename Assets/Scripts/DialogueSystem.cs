using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    GameObject objectOption1;
    GameObject objectOption2;
    GameObject objectOption3;
    GameObject objectOption4;

    Text textOption1;
    Text textOption2;
    Text textOption3;
    Text textOption4;
    Text textDialogue;

    int stage = 0;
    int reputation = 0;

	// Use this for initialization
    void OnEnable()
    {
        if (objectOption1 == null)
        {
            GatherObjects();
        }

        ResetDialogue();
    }

    void GatherObjects()
    {
        objectOption1 = transform.GetChild(3).gameObject;
        objectOption2 = transform.GetChild(4).gameObject;
        objectOption3 = transform.GetChild(5).gameObject;
        objectOption4 = transform.GetChild(6).gameObject;

        textDialogue = transform.GetChild(2).GetComponent<Text>();
        textOption1 = objectOption1.transform.GetChild(0).GetComponent<Text>();
        textOption2 = objectOption2.transform.GetChild(0).GetComponent<Text>();
        textOption3 = objectOption3.transform.GetChild(0).GetComponent<Text>();
        textOption4 = objectOption4.transform.GetChild(0).GetComponent<Text>();
    }

    string GetRandomOpening()
    {
        string openingText = "";

        int randomOpening = Random.Range(0, 3);

        switch (randomOpening)
        {
            case 0:
                openingText = "What can I serve you today, Sheriff?"; break;
            case 1:
                openingText = "What's your poison, Sheriff?"; break;
            case 2:
                openingText = "Welcome, Sheriff."; break;
            default:
                openingText = "ERROR"; break;
        }

        return openingText;
    }

    string GetRandomQuestion()
    {
        string openingText = "";

        int randomOpening = Random.Range(0, 3);

        switch (randomOpening)
        {
            case 0:
                openingText = "I'm here to serve drinks, but I might know a thing or two."; break;
            case 1:
                openingText = "What do you want to know?"; break;
            case 2:
                openingText = "Do you want your answers straight up, or with a twist?"; break;
            default:
                openingText = "ERROR"; break;
        }

        return openingText;
    }

    public void ActivateOption(int option)
    {
        if (stage == 0)
        {
            if (option == 1)
            {
                stage = 1;
            }
            else if (option == 2)
            {
                stage = 4;
            }
            else
            {
                stage = -1;
            }
        }
        else if (stage == 1)
        {
            if (option == 1 && reputation == 0)
            {
                stage = 2;
            }
            else if (option == 1 && reputation != 0)
            {
                stage = 3;
            }
            else if (option == 2)
            {
                stage = 4;
            }
            else if (option == 3)
            {
                stage = 5;
            }
            else
            {
                stage = 0;
            }
        }

        if (stage != -1)
        {
            UpdateDialogue();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void UpdateDialogue()
    {
        if (stage == 0)
        {
            ResetDialogue();
        }
        else if (stage == 1)
        {
            AskInformation();
        }
        else if (stage == 2)
        {
            RefuseAnswer();
        }
    }

    void ResetDialogue()
    {
        objectOption1.SetActive(true);
        objectOption2.SetActive(true);
        objectOption3.SetActive(false);

        textDialogue.text = GetRandomOpening();
        textOption1.text = "Ask for information";
        textOption2.text = "Accuse as the target";
        textOption3.text = "";
        textOption4.text = "Goodbye";

        stage = 0;
    }

    void AskInformation()
    {
        objectOption1.SetActive(true);
        objectOption2.SetActive(true);
        objectOption3.SetActive(true);

        textDialogue.text = GetRandomQuestion();
        textOption1.text = "Ask about the target";
        textOption2.text = "Refresh his memory with cash";
        textOption3.text = "Smash a bottle to his head";
        textOption4.text = "Return";
    }

    void RefuseAnswer()
    {
        int randomOpening = Random.Range(0, 3);
        switch (randomOpening)
        {
            case 0:
                textDialogue.text = "I'm sorry, it seems my memory is foggy."; break;
            case 1:
                textDialogue.text = "A Benjamin might jog my memory."; break;
            case 2:
                textDialogue.text = "Sorry, sheriff. I'm more scared of him than of you."; break;
            default:
                textDialogue.text = "ERROR"; break;
        }
    }

    void GiveAnswerBribe()
    {
        objectOption1.SetActive(false);
        objectOption2.SetActive(false);
        objectOption3.SetActive(false);

        textDialogue.text = "The target is a cat.";
    }
}
