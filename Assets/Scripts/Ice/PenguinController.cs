using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PenguinController : MonoBehaviour {

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }
}
