using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Image imageLoading;
    public Text textLoading;

    private bool loadScene = false;

    private IEnumerator coroutine;

	// Use this for initialization
	void Start ()
    {
        UpdateLoadingText();
        StartCoroutine(LoadNewScene());
    }
	
	// Update is called once per frame
	void Update ()
    {
        imageLoading.color = Color.Lerp(Color.white, Color.clear, Mathf.PingPong(Time.time, 1));
    }

    void UpdateLoadingText()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        switch (textLoading.text)
        {
            case "Loading":
                textLoading.text = "Loading."; break;
            case "Loading.":
                textLoading.text = "Loading.."; break;
            case "Loading..":
                textLoading.text = "Loading..."; break;
            case "Loading...":
                textLoading.text = "Loading"; break;
            default:
                textLoading.text = "Loading"; break;
        }

        coroutine = WaitOneTick();
        StartCoroutine(coroutine);
    }

    IEnumerator WaitOneTick()
    {
        yield return new WaitForSeconds(1);
        UpdateLoadingText();
    }

    IEnumerator LoadNewScene()
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync("PuzzlePrototype");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
