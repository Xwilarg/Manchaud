using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OrcController : MonoBehaviour {

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
    private float jumpTime;
    [SerializeField]
    private float jumpForce;

    private float attackTimer;
    private float? jumpTimer;
    private float? killTimer;

    private GameObject player;
    private Node nextNode;
    private Rigidbody2D rb;

    private void SetAttackTimer()
    {
        attackTimer = Random.Range(minTimeJump, maxTimeJump);
    }

    private void Start()
    {
        Debug.Assert(minTimeJump < maxTimeJump, "min time must be < to max time");
        nextNode = firstNode.GetNextNode();
        rb = GetComponent<Rigidbody2D>();
        SetAttackTimer();
        jumpTimer = null;
        killTimer = null;
        player = GameObject.FindGameObjectWithTag("Player");
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

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0f)
        {
            SetAttackTimer();
            jumpTimer = 2f;
        }
        if (jumpTimer == null)
        {
            float xDir = GetXDirection();
            float yDir = GetYDirection();
            if (xDir == 0 && yDir == 0)
                nextNode = nextNode.GetNextNode();
            else
                rb.velocity = new Vector2(xDir * speed * Time.deltaTime, yDir * speed * Time.deltaTime);
        }
        else if (killTimer == null)
        {
            rb.velocity = Vector2.zero;
            jumpTimer -= Time.deltaTime;
            if (jumpTimer < 0f)
            {
                Vector2 dir = (transform.position - player.transform.position).normalized;
                rb.AddForce(-dir * jumpForce, ForceMode2D.Impulse);
                killTimer = jumpTime;
            }
        }
        else
        {
            killTimer -= Time.deltaTime;
            if (killTimer < 0f)
            {
                killTimer = null;
                jumpTimer = null;
            }
        }
    }
}
