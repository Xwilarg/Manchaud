using UnityEngine;

public class LaunchFish : MonoBehaviour
{
    [SerializeField]
    private GameObject fish, dest;
    [SerializeField]
    private float minDist, maxDist;
    private float timer;
    private PenguinController player;

    private void GenerateTimer()
    {
        timer = Random.Range(minDist, maxDist);
    }

    private void Start()
    {
        GenerateTimer();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinController>();
    }

    private void Update()
    {
        if (!player.IsAlive())
            return;
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
