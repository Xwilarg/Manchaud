using UnityEngine;

public class Hover : MonoBehaviour
{
    private bool goUp;
    private const float offset = 0.1f;
    private const float speed = 0.6f;
    private float orrY;

    private void Start()
    {
        goUp = true;
        orrY = transform.position.y;
    }

    private void Update()
    {
        transform.Translate(new Vector2(0f, ((goUp) ? (speed) : (-speed)) * Time.deltaTime));
        if (transform.position.y >= orrY + offset)
            goUp = false;
        else if (transform.position.y <= orrY - offset)
            goUp = true;
    }
}
