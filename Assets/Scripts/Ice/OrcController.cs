﻿using System.Collections.Generic;
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
    private GameObject spriteS;
    [SerializeField]
    private GameObject spriteN;
    [SerializeField]
    private GameObject spriteW;
    [SerializeField]
    private GameObject spriteE;
    [SerializeField]
    private GameObject spriteNE;
    [SerializeField]
    private GameObject spriteSE;
    [SerializeField]
    private GameObject spriteNW;
    [SerializeField]
    private GameObject spriteSW;
    [SerializeField]
    private Sprite hiddenS;
    [SerializeField]
    private Sprite hiddenN;
    [SerializeField]
    private Sprite hiddenW;
    [SerializeField]
    private Sprite hiddenE;
    [SerializeField]
    private Sprite hiddenNE;
    [SerializeField]
    private Sprite hiddenSE;
    [SerializeField]
    private Sprite hiddenNW;
    [SerializeField]
    private Sprite hiddenSW;

    private Dictionary<Vector2Int, GameObject> allSprites;
    private Dictionary<Vector2Int, Sprite> allHidens;
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
        allSprites = new Dictionary<Vector2Int, GameObject>()
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
        allHidens = new Dictionary<Vector2Int, Sprite>()
        {
            { new Vector2Int(0, -1), hiddenS },
            { new Vector2Int(0, 1), hiddenN },
            { new Vector2Int(1, 0), hiddenE },
            { new Vector2Int(-1, 0), hiddenW },
            { new Vector2Int(1, -1), hiddenSE },
            { new Vector2Int(-1, -1), hiddenSW },
            { new Vector2Int(1, 1), hiddenNE },
            { new Vector2Int(-1, 1), hiddenNW },
        };
        nodes = new List<Node>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Node"))
        {
            nodes.Add(go.GetComponent<Node>());
        }
        firstNode = GoClosest();
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
        sr.sprite = allHidens[new Vector2Int(GetXDirection(player.transform.position), GetYDirection(player.transform.position))];
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
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
            sr.sprite = allHidens[new Vector2Int(xDir, yDir)];
            Destroy(GetComponent<PolygonCollider2D>());
            gameObject.AddComponent<PolygonCollider2D>();
            rb.velocity = new Vector2(xDir * speed * Time.deltaTime, yDir * speed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        rb.velocity = Vector2.zero;
        jumpTimer -= Time.deltaTime;
        if (jumpTimer < 0f)
        {
            GameObject go = allSprites[new Vector2Int(GetXDirection(player.transform.position), GetYDirection(player.transform.position))];
            sr.sprite = go.GetComponent<SpriteRenderer>().sprite;
            sr.GetComponent<PolygonCollider2D>().points = go.GetComponent<PolygonCollider2D>().points;
            rb.AddForce(-jumpDir * jumpForce, ForceMode2D.Impulse);
            killTimer = jumpTime;
            SetSound(Action.slide);
        }
    }

    private Node GoClosest()
    {
        float? minDistance = null;
        Node next = null;
        foreach (Node n in nodes)
        {
            float currDistance = Vector2.Distance(transform.position, n.transform.position);
            if (minDistance == null || currDistance < minDistance)
            {
                minDistance = currDistance;
                next = n;
            }
        }
        return (next);
    }

    private void GoBackSwim()
    {
        killTimer -= Time.deltaTime;
        if (killTimer < 0f)
        {
            killTimer = null;
            jumpTimer = null;
            nextNode = GoClosest();
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
