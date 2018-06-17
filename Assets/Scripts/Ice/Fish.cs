using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    private Score score;
    [SerializeField]
    private GameObject popup;
    [SerializeField]
    private AudioClip outOfWaterClip, beingEatClip;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();
        source.clip = outOfWaterClip;
        source.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ice"))
            rb.drag = 5f;
        else if (collision.CompareTag("Player"))
        {
            Instantiate(popup, transform.position, Quaternion.identity);
            score.AddScore(25f);
            source.clip = outOfWaterClip;
            source.Play();
            transform.position = new Vector2(-100f, -100f);
            Destroy(gameObject, source.clip.length);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.CompareTag("Ice"))
            Destroy(gameObject);
    }
}
