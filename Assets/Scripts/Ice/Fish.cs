using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    private Score score;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ice"))
            rb.drag = 5f;
        else if (collision.CompareTag("Player"))
        {
            score.AddScore(25f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.CompareTag("Ice"))
            Destroy(gameObject);
    }
}
