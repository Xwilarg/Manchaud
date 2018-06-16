using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class OrcController : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField]
    private Node firstNode;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float step;
    [Header("Attack")]
    [SerializeField]
    private float minTimeJump;
    [SerializeField]
    private float maxTimeJump;
    [SerializeField]
    private float jumpForce;
    [Header("Sounds")]
    [SerializeField]
    private AudioClip swimSound;
    [SerializeField]
    private AudioClip attackSound;
    [SerializeField]
    private AudioClip slideSound;

    private AudioSource source;
    private float attackTimer;
    private float? jumpTimer;
    private float? killTimer;
    private Vector2 jumpDir;

    private const float jumpTime = 0.5f;

    private GameObject player;
    private Node nextNode;
    private Rigidbody2D rb;

    private List<Node> nodes;

    private enum Action
    {
        swim,
        attack,
        slide
    }

    private void SetAttackTimer()
    {
        attackTimer = Random.Range(minTimeJump, maxTimeJump);
    }

    private void Start()
    {
        Debug.Assert(minTimeJump < maxTimeJump, "min time must be < to max time");
        nodes = new List<Node>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Node"))
        {
            nodes.Add(go.GetComponent<Node>());
        }
        nextNode = firstNode.GetNextNode();
        rb = GetComponent<Rigidbody2D>();
        SetAttackTimer();
        jumpTimer = null;
        killTimer = null;
        player = GameObject.FindGameObjectWithTag("Player");
        source = GetComponent<AudioSource>();
        SetSound(Action.swim);
    }

    private void SetSound(Action currAction)
    {
        switch (currAction)
        {
            case Action.attack:
                source.clip = attackSound;
                source.loop = false;
                break;

            case Action.slide:
                source.clip = slideSound;
                source.loop = false;
                break;

            case Action.swim:
                source.clip = swimSound;
                source.loop = true;
                break;
        }
        source.Play();
    }

    private int GetXDirection()
    {
        if (Mathf.Abs(transform.position.x - nextNode.transform.position.x) < step)
            return (0);
        if (transform.position.x < nextNode.transform.position.x)
            return (1);
        return (-1);
    }

    private int GetYDirection()
    {
        if (Mathf.Abs(transform.position.y - nextNode.transform.position.y) < step)
            return (0);
        if (transform.position.y < nextNode.transform.position.y)
            return (1);
        return (-1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Rigidbody2D>().AddTorque(float.MaxValue);
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        // Prepare attack
        if (attackTimer < 0f)
        {
            SetAttackTimer();
            jumpTimer = 2f;
            jumpDir = (transform.position - player.transform.position).normalized;
            SetSound(Action.attack);
        }
        // Swim
        if (jumpTimer == null)
        {
            float xDir = GetXDirection();
            float yDir = GetYDirection();
            if (xDir == 0 && yDir == 0)
                nextNode = nextNode.GetNextNode();
            else
                rb.velocity = new Vector2(xDir * speed * Time.deltaTime, yDir * speed * Time.deltaTime);
        }
        // Jump
        else if (killTimer == null)
        {
            rb.velocity = Vector2.zero;
            jumpTimer -= Time.deltaTime;
            if (jumpTimer < 0f)
            {
                rb.AddForce(-jumpDir * jumpForce, ForceMode2D.Impulse);
                killTimer = jumpTime;
                SetSound(Action.slide);
            }
        }
        // Go back to swim
        else
        {
            killTimer -= Time.deltaTime;
            if (killTimer < 0f)
            {
                killTimer = null;
                jumpTimer = null;
                float? minDistance = null;
                foreach (Node n in nodes)
                {
                    float currDistance = Vector2.Distance(transform.position, n.transform.position);
                    if (minDistance == null || currDistance < minDistance)
                    {
                        minDistance = currDistance;
                        nextNode = n;
                    }
                }
                SetSound(Action.swim);
            }
        }
    }
}
