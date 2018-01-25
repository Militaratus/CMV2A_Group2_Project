using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class LoadingManager : MonoBehaviour
{
    // Managers
    GameManager managerGame;
    EscapePlan myPlan;

    // Internal
    public Image exampleImage;
    public Text exampleTitle;
    public Text exampleDescription;

    // Use this for initialization
    private void Awake()
    {
#if UNITY_EDITOR
        if (GameObject.Find("GameManager"))
        {
            managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        else
        {
            GameObject newGameManagerPrefab = Resources.Load("Common/GameManager") as GameObject;
            GameObject newGameNager = Instantiate(newGameManagerPrefab);
            newGameNager.name = "GameManager";
            managerGame = newGameNager.GetComponent<GameManager>();
        }
#else
        managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
#endif
        managerGame.LoadTasks();
        myPlan = managerGame.activeEscapePlan;
        LoadExampleText();
    }

    // Use this for second initialization
    void Start ()
    {
        StartCoroutine(LoadNewScene());
    }

    // Grab the escape plan text
    void LoadExampleText()
    {
        exampleTitle.text = myPlan.title;
        exampleDescription.text = myPlan.description.Replace("NEWLINE", "\n");
    }

    // Loading Async from Unity Script Reference/Manual, slightly adapted for this project
    IEnumerator LoadNewScene()
    {
        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(managerGame.nextScene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
