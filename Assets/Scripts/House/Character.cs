using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Character : MonoBehaviour {

    private Rigidbody2D rb;
    private bool stairs;
    [SerializeField]
    private Room room;
    [SerializeField]
    private float speed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        room = other.GetComponent<Room>();
    }

    // Use this for initialization
    private void Start () {
        stairs = room.HasStairs();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void Update () {
        if (stairs)
        {
            rb.MovePosition(room.GetDown().GetComponent<Rigidbody2D>().position);
        }
        else
        {
            if (room.GetLeft() != null)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, 0); // x > 0 right || x < 0 left
            }
            else if (room.GetRight() != null)
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, 0); // x > 0 right || x < 0 left
            }
        }
    }
}
