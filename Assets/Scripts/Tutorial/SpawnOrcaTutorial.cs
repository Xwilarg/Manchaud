using UnityEngine;

public class SpawnOrcaTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject orca;
    private bool isSpawned;

    private void Start()
    {
        isSpawned = false;
    }

    public void Spawn()
    {
        if (!isSpawned)
            Instantiate(orca, transform.position, Quaternion.identity);
        isSpawned = true;
    }
}
