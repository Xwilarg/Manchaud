using UnityEngine;

public class GameManager : MonoBehaviour {
    [Range(0.01f, 1f)]
    [SerializeField]
    private float soundVolume;
    [SerializeField]
    private Difficulties difficulty;
    enum Difficulties { EASY=20, MEDIUM=50, HARD=80, INSANE=100 }
}
