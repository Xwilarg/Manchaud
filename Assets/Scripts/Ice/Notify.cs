using UnityEngine;

public class Notify : MonoBehaviour
{
    private float timer;

    private void Start()
    {
        timer = 2f;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
            Destroy(gameObject);
    }
}
