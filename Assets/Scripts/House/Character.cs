using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {

    private Rigidbody2D rb;
    private bool stairs;
    [SerializePrivateVariables]
    private Collider room;

    private void OnTriggerEnter(Collider other)
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
        //if (stairs)

        //else
        //    rb.velocity = new Vector2(5 * Time.deltaTime, 0); // x > 0 right || x < 0 left
	}
}
