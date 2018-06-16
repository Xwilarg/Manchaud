using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("main");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
