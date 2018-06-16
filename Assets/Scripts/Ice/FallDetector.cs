using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Rigidbody2D>().AddTorque(float.MaxValue);
    }
}
