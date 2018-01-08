using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Image imageLoading;

    //public Image exampleImage;
    public Text exampleTitle;
    public Text exampleDescription;

	// Use this for initialization
	void Start ()
    {
        LoadExampleText();
        StartCoroutine(LoadNewScene());
    }

    void LoadExampleText()
    {
        exampleTitle.text = "Operation TopGun";
        exampleDescription.text = "<b>Steps:</b>\n- Find the backpack\n- Get into the solitary\n- Free a chicken\n- Catch the chicken\n- Put it in the back pack\n- Wait for the night\n- Get on top of the water tower\n- Equip the chicken glider\n- Jump and glide to freedom\n\n<b>Core items:</b>\n- Backpack\n- Chicken\n- keys\n";
    }
	
	// Update is called once per frame
	void Update ()
    {
        imageLoading.color = Color.Lerp(Color.white, Color.clear, Mathf.PingPong(Time.time, 1));
    }

    IEnumerator LoadNewScene()
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("PrototypeNew");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
