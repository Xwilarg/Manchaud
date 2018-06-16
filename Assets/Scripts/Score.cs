using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour
{
    [SerializeField]
    private int scoreStep;
    private int score;
    private PenguinController penguin;
    private bool uploadDone;

    public void Start()
    {
        score = 0;
        penguin = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinController>();
        uploadDone = false;
    }

    public void Update()
    {
        score += scoreStep * (int)Time.deltaTime;
        if (!penguin.IsAlive())
        {
            StartCoroutine(Upload());
            if (uploadDone)
                SceneManager.LoadScene("MainMenu");
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
        uploadDone = true;
    }
}