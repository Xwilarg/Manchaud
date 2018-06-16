using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private Room room, oldRoom;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Sprite spriteLeft, spriteRight;
    private SpriteRenderer sr;
    private Directions direction;
    private bool tookStairs;
    private enum Directions { LEFT, RIGHT }
    [SerializeField]
    private Vector3 firstFloorOffset, secondFloorOffset;
    [Range(0, 100)]
    [SerializeField]
    private int switchOnRate;
    [SerializeField]
    private AudioClip[] walkingClips;
    private AudioSource audioSrc;
    private int step;
    private float timer;
    [SerializeField]
    private float waitingTime;
    [Range(0, 100)]
    [SerializeField]
    private int takeStairsRate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Room"))
        {
            Stop();
            oldRoom = room;
            room = other.GetComponent<Room>();
            timer = 0f;

            SwitchOnDevices();
            TakeStairs();
        }
    }

    private void SwitchOnDevices()
    {
        Transform[] devices = room.GetComponentsInChildren<Transform>();
        foreach (Transform device in devices)
        {
            if (Random.Range(1, 100) < switchOnRate)
            {
                SwitchLight sl = device.GetComponent<SwitchLight>();
                if (sl != null)
                    sl.SwitchOn();
            }
        }
    }

    private void TakeStairs()
    {
        Debug.Log(Random.Range(0, 100));
        if (Random.Range(0, 100) > takeStairsRate)
            return;
        if (!tookStairs && room.HasStairs())
        {
            if (room.GetDown() != null && room.GetDown() != oldRoom)
                rb.MovePosition(room.GetDown().transform.position - firstFloorOffset);
            else if (room.GetUp() != null && room.GetUp() != oldRoom)
                rb.MovePosition(room.GetUp().transform.position - secondFloorOffset);
            //tookStairs = true;
        }
    }

    private void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        direction = Directions.RIGHT;
        audioSrc = GetComponent<AudioSource>();
        step = 0;
        timer = 1f;
	}
	
	private void Update () {
        if (step++ % 30 == 0)
            audioSrc.PlayOneShot(walkingClips[Random.Range(0, 5)], 0.3f);
        if (Wait())
            Move();
    }

    private bool Wait()
    {
        timer += Time.deltaTime;
        return timer >= waitingTime;
    }

    private void Move()
    {
        if (direction != Directions.LEFT && room.GetLeft() != null)
        {
            direction = Directions.LEFT;
            sr.sprite = spriteLeft;
            rb.velocity = new Vector2(-speed * Time.deltaTime, 0); // speed < 0 == left
        }
        else if (direction != Directions.RIGHT && room.GetRight() != null)
        {
            direction = Directions.RIGHT;
            sr.sprite = spriteRight;
            rb.velocity = new Vector2(speed * Time.deltaTime, 0); // speed > 0 == right
        }
    }

    private void Stop()
    {
        rb.velocity = new Vector2(0, 0);
    }
}
