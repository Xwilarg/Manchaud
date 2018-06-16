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
    AchievementManager am;
    
	private void Start () {
        hits = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        am = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<AchievementManager>();
	}

    private void OnMouseDown()
    {
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
