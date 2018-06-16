using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateIce : MonoBehaviour {
    private Rigidbody2D rb;
    [Range(0, 5)]
    [SerializeField]
    private float rotationSpeed;
    private float timer;

	public void Start () {
        rb = GetComponent<Rigidbody2D>();
        timer = 0f;
	}

    public void Update()
    {
        Debug.Log(timer);
        timer += Time.deltaTime;
        if (rb != null)
        {
            if (timer > 3f)
            {
                rotationSpeed *= -1;
                timer = -3f;
            }
            rb.angularVelocity = rotationSpeed;
        }
    }
}
