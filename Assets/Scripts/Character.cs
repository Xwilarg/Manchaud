using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {

    private Rigidbody2D rb;
    private bool stairs;

    private void OnTriggerEnter(Collider other)
    {
        if (true/*other.GetComponent<ScriptableObject>().HasStairs()*/)
            stairs = true;
        else
            stairs = false;
    }

    // Use this for initialization
    private void Start () {
        stairs = false;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void Update () {
        if (stairs)
            rb.velocity = new Vector2(0, 2);
        else
            rb.velocity = new Vector2(5, 0);
	}
}
