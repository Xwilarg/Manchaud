using UnityEngine;

public class MoveUnicorn : MonoBehaviour {

    private bool doesMove;
    private Vector3 basePos;

    private void Start()
    {
        doesMove = false;
        basePos = transform.position;
    }

    public void Move()
    {
        doesMove = true;
        transform.position = basePos;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Unicorn"))
            Move();
        if (doesMove)
            transform.Translate(new Vector2(2f * Time.deltaTime, 0f));
    }
}
