using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour {

    private Rigidbody2D rb;
    private bool stairs;
    [SerializeField]
    private Room room, oldRoom;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Sprite spriteLeft, spriteRight;
    private SpriteRenderer renderer;
    private int direction;
    private bool tookStairs;
    private enum Directions { LEFT, RIGHT };
    [SerializeField]
    private Vector3 offset;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Room"))
        {
            oldRoom = room;
            room = other.GetComponent<Room>();
            if (tookStairs == false && room.HasStairs())
            {
                if (room.GetDown() != null && room.GetDown() != oldRoom)
                    rb.MovePosition(room.GetDown().transform.position - offset);
                else if (room.GetUp() != null && room.GetUp() != oldRoom)
                    rb.MovePosition(room.GetDown().transform.position);
                tookStairs = true;
            }
        }
    }

    private void Start () {
        stairs = room.HasStairs();
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        direction = (int)Directions.RIGHT;
        tookStairs = true;
	}
	
	private void Update () {
            tookStairs = false;
        if (direction != (int)Directions.LEFT && room.GetLeft() != null)
        {
            direction = (int)Directions.LEFT;
            renderer.sprite = spriteLeft;
            rb.velocity = new Vector2(-speed * Time.deltaTime, 0); // x > 0 right || x < 0 left
        }
        else if (direction != (int)Directions.RIGHT && room.GetRight() != null)
        {
            direction = (int)Directions.RIGHT;
            renderer.sprite = spriteRight;
            rb.velocity = new Vector2(speed * Time.deltaTime, 0); // x > 0 right || x < 0 left
        }
    }
}
