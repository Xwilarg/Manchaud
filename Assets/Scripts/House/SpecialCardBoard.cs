using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class SpecialCardBoard : MonoBehaviour {
    private int hits;
    [SerializeField]
    private int breakLimit;
    [SerializeField]
    private Sprite breakSprite;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private AudioClip breakClip;
    private AudioSource audioSource;
    private AchievementManager am;
    private PenguinController player;
    
	private void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinController>();
        hits = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        am = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<AchievementManager>();
	}

    private void OnMouseDown()
    {
        if (player.IsAlive())
            ++hits;
    }
    
    private void Update () {
        if (hits == breakLimit)
            Break();
	}

    public void Break()
    {
        hits++;
        spriteRenderer.sprite = breakSprite;
        audioSource.PlayOneShot(breakClip);
        am.Create("It's not a cardboard!", "Look like I broke grand mother's porcelain.");
    }
}
