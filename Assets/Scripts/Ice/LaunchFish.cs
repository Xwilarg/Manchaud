using UnityEngine;

public class LaunchFish : MonoBehaviour
{
    [SerializeField]
    private GameObject fish, dest;
    [SerializeField]
    private float minDist, maxDist;
    private float timer;

    private void GenerateTimer()
    {
        timer = Random.Range(minDist, maxDist);
    }

    private void Start()
    {
        GenerateTimer();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            GenerateTimer();
            Vector2 dir = (transform.position - dest.transform.position).normalized;
            GameObject go =Instantiate(fish, transform.position, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().AddForce(-dir * 5f, ForceMode2D.Impulse);
        }
    }
}
