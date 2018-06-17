using UnityEngine;

public class SpawnOrca : MonoBehaviour
{
    [SerializeField]
    private GameObject orca;

    private float timer;
    private PenguinController player;

    private void Start()
    {
        Instantiate(orca, transform.position, Quaternion.identity);
        timer = 0f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinController>();
    }

    private void Update()
    {
        if (!player.IsAlive())
            return;
        timer += Time.deltaTime;
        if (timer > 30f)
        {
            Instantiate(orca, transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}
