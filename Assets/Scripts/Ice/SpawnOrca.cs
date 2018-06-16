using UnityEngine;

public class SpawnOrca : MonoBehaviour {

    [SerializeField]
    private GameObject orca;

    private float timer;

    private void Start()
    {
        Instantiate(orca, transform.position, Quaternion.identity);
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 30f)
        {
            Instantiate(orca, transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}
