using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    // Managers
    GameManager managerGame;
    EscapePlan myPlan;

    public Image exampleImage;
    public Text exampleTitle;
    public Text exampleDescription;

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

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(LoadNewScene());
    }

    void LoadExampleText()
    {
        exampleTitle.text = myPlan.title;
        exampleDescription.text = myPlan.description.Replace("NEWLINE", "\n");
    }

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
