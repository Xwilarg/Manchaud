using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
	public void ResetScene()
    {
        SceneManager.LoadScene("main");
    }
}
