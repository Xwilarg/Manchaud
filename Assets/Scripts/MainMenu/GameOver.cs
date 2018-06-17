using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameOver : MonoBehaviour
{
    private Text text;
    private Score score;

    private void Start()
    {
        text = GetComponentsInChildren<Text>()[0];
        score = FindObjectOfType<Score>();
    }
    private void Update()
    {
        text.text = "Score: " + score.GetScore();
    }

    public void Retry()
    {
        SceneManager.LoadScene("main");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
