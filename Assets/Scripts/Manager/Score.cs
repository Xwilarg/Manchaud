using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour
{
    [SerializeField]
    private int scoreStep;
    private float score;
    private PenguinController penguin;
    private bool uploadDone;
    private bool uploadStart;
    private Text text;
    private Save save;

    private void Start()
    {
        score = 0;
        penguin = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinController>();
        text = GetComponent<Text>();
        save = FindObjectOfType<Save>();
    }

    private void Update()
    {
        if (penguin.IsAlive())
            score += (float)scoreStep * Time.deltaTime;
        text.text = "Score: " + (int)score;
        if (save != null)
        {
            if ((int)score > save.bestScore)
                save.bestScore = (int)score;
            text.text += "\nBest score: " + save.bestScore;
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

    public int GetScore()
    {
        return (int)score;
    }
}