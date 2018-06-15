using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OrcController : MonoBehaviour {

    [SerializeField]
    private Node firstNode;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float step;

    private Node nextNode;
    private Rigidbody2D rb;

    private void Start()
    {
        nextNode = firstNode.GetNextNode();
        rb = GetComponent<Rigidbody2D>();
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
        float xDir = GetXDirection();
        float yDir = GetYDirection();
        if (xDir == 0 && yDir == 0)
            nextNode = nextNode.GetNextNode();
        else
            rb.velocity = new Vector2(xDir * speed * Time.deltaTime, yDir * speed * Time.deltaTime);
    }
}
