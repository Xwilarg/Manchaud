using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class OrcController : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float step;
    [SerializeField]
    private float speedMultiplicator;
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
    [Header("Sprites")]
    [SerializeField]
    private Sprite spriteS;
    [SerializeField]
    private Sprite spriteN;
    [SerializeField]
    private Sprite spriteW;
    [SerializeField]
    private Sprite spriteE;
    [SerializeField]
    private Sprite spriteNE;
    [SerializeField]
    private Sprite spriteSE;
    [SerializeField]
    private Sprite spriteNW;
    [SerializeField]
    private Sprite spriteSW;

    private Dictionary<Vector2Int, Sprite> allSprites;
    private SpriteRenderer sr;
    private PolygonCollider2D coll;
    private AudioSource source;
    private float attackTimer;
    private float? jumpTimer;
    private float? killTimer;
    private Vector2 jumpDir;

    private const float jumpTime = 1.0f;

    private GameObject player;
    private Node nextNode;
    private Rigidbody2D rb;

    private List<Node> nodes;
    private Node firstNode;

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
        allSprites = new Dictionary<Vector2Int, Sprite>()
        {
            { new Vector2Int(0, -1), spriteS },
            { new Vector2Int(0, 1), spriteN },
            { new Vector2Int(1, 0), spriteE },
            { new Vector2Int(-1, 0), spriteW },
            { new Vector2Int(1, -1), spriteSE },
            { new Vector2Int(-1, -1), spriteSW },
            { new Vector2Int(1, 1), spriteNE },
            { new Vector2Int(-1, 1), spriteNW },
        };
        nodes = new List<Node>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Node"))
        {
            nodes.Add(go.GetComponent<Node>());
        }
        firstNode = nodes[0];
        nextNode = firstNode.GetNextNode();
        rb = GetComponent<Rigidbody2D>();
        SetAttackTimer();
        jumpTimer = null;
        killTimer = null;
        player = GameObject.FindGameObjectWithTag("Player");
        source = GetComponent<AudioSource>();
        SetSound(Action.swim);
        sr = GetComponent<SpriteRenderer>();
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

    private int GetXDirection(Vector2 dest)
    {
        if (Mathf.Abs(transform.position.x - dest.x) < step)
            return (0);
        if (transform.position.x < dest.x)
            return (1);
        return (-1);
    }

    private int GetYDirection(Vector2 dest)
    {
        if (Mathf.Abs(transform.position.y - dest.y) < step)
            return (0);
        if (transform.position.y < dest.y)
            return (1);
        return (-1);
    }

    private void PrepareAttack()
    {
        SetAttackTimer();
        jumpTimer = 2f;
        jumpDir = (transform.position - player.transform.position).normalized;
        sr.sprite = allSprites[new Vector2Int(GetXDirection(player.transform.position), GetYDirection(player.transform.position))];
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        SetSound(Action.attack);
    }

    private void Swim()
    {
        int xDir = GetXDirection(nextNode.transform.position);
        int yDir = GetYDirection(nextNode.transform.position);
        if (xDir == 0 && yDir == 0)
            nextNode = nextNode.GetNextNode();
        else
        {
            sr.sprite = allSprites[new Vector2Int(xDir, yDir)];
            Destroy(GetComponent<PolygonCollider2D>());
            gameObject.AddComponent<PolygonCollider2D>();
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
            rb.velocity = new Vector2(xDir * speed * Time.deltaTime, yDir * speed * Time.deltaTime);
        }
    }

    private void Jump()
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

    private void GoBackSwim()
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

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0f)
            PrepareAttack();
        if (jumpTimer == null)
            Swim();
        else if (killTimer == null)
            Jump();
        else
            GoBackSwim();
    }
}
