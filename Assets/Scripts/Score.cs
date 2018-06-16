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
    private bool uploadStart;

    public void Start()
    {
        score = 0;
        penguin = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinController>();
        uploadDone = false;
        uploadStart = false;
    }

    public void Update()
    {
        score += scoreStep * (int)Time.deltaTime;
        if (!penguin.IsAlive())
        {
            if (!uploadStart)
                StartCoroutine(Upload());
        }
    }

    public IEnumerator Upload()
    {
        Debug.Log("Start upload");
        WWWForm form = new WWWForm();
        form.AddField("name", "userTest");
        form.AddField("score", score.ToString());
        form.AddField("version", "0.1.9");

        UnityWebRequest www = UnityWebRequest.Post("https://zirk.eu/Manchaud/manchaud.php", form);
        uploadStart = true;
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            uploadDone = true;
            Debug.Log("Form upload complete!");
        }
    }
}