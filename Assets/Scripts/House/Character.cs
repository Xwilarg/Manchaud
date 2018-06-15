using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {

    private Rigidbody2D rb;
    private bool stairs;
    [SerializeField]
    private Collider2D room;

    private void OnTriggerEnter2D(Collider2D other)
    {
        room = other;
    }

    // Use this for initialization
    private void Start () {
        stairs = true;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void Update () {
        if (stairs)
            rb.MovePosition(room.GetComponent<Room>().Down.GetComponent<Rigidbody2D>().position);
        else
            rb.velocity = new Vector2(5 * Time.deltaTime, 0); // x > 0 right || x < 0 left
	}
}
