using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class Score : MonoBehaviour
{
    [SerializeField]
    private int scoreStep;
    private int score;
    private GameObject penguin;

    public void Start()
    {
        score = 0;
        penguin = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        score += scoreStep * (int)Time.deltaTime;
        if (!penguin.GetComponent<PenguinController>().IsAlive())
        {
            StartCoroutine(Upload());
            Application.Quit();
        }
    }

    public IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "userTest");
        form.AddField("score", score);
        form.AddField("version", "0.1.9");

        UnityWebRequest www = UnityWebRequest.Post("https://zirk.eu/Manchaud/manchaud.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}