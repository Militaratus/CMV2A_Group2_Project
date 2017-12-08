using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Text textQuote;
    public Text textQuoter;
    public Image background;
    public Transform sunLight;

    public int step = 0;
    private float timer = 0;
    private float extendedTimer = 0;
    private IEnumerator coroutine;

    Quaternion sunNight = new Quaternion(1.0f, 0.0f, 0.0f, -0.3f);
    Quaternion sunDay = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);


    // Use this for initialization
    void Start ()
    {
        textQuote.color = Color.clear;
        textQuoter.color = Color.clear;
        background.color = Color.black;
        sunLight.rotation = sunNight;

        coroutine = TimedScript();
        StartCoroutine(coroutine);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (step == 0)
        {
            textQuote.color = Color.Lerp(Color.clear, Color.white, timer);
        }
        if (step == 1)
        {
            textQuoter.color = Color.Lerp(Color.clear, Color.white, timer);
        }
        if (step == 2)
        {
            textQuote.color = Color.Lerp(Color.white, Color.clear, timer);
            textQuoter.color = Color.Lerp(Color.white, Color.clear, timer);
            background.color = Color.Lerp(Color.black, Color.clear, timer);
        }

            sunLight.rotation = Quaternion.Lerp(sunNight, sunDay, extendedTimer);
            extendedTimer += Time.deltaTime / 60.0f;

        timer += Time.deltaTime / 2.0f;
    }

    IEnumerator TimedScript()
    {
        step = 0;
        timer = 0;
        yield return new WaitForSeconds(2);
        step = 1;
        timer = 0;
        yield return new WaitForSeconds(2);
        step = 2;
        timer = 0;
    }
}
