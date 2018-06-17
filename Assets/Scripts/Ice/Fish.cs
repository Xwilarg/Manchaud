using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("BB");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AA");
        if (collision.CompareTag("Ice"))
        {
            rb.drag = 5f;
            Debug.Log("TE");
        }
    }
}
