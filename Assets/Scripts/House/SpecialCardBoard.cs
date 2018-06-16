using UnityEngine;

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

	// Use this for initialization
	void Start () {
        hits = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
	}

    public void OnMouseDown()
    {
        Debug.Log("Hit");
        ++hits;
    }

    // Update is called once per frame
    public void Update () {
        if (hits == breakLimit)
            Break();
	}

    public void Break()
    {
        spriteRenderer.sprite = breakSprite;
        audioSource.PlayOneShot(breakClip);
        Debug.Log("It's not a cardboard");
    }
}
