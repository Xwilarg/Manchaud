using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PenguinController : MonoBehaviour {

    private Rigidbody2D rb;
    [SerializeField]
    private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(hor * speed * Time.deltaTime, ver * speed * Time.deltaTime);
    }
}
