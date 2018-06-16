using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsPanel;

	public void Play()
    {
        SceneManager.LoadScene("main");
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
