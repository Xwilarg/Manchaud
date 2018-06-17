using UnityEngine;

public class Save : MonoBehaviour
{
    public int bestScore;

	private void Start () {
        DontDestroyOnLoad(gameObject);
        bestScore = 0;
	}
}
