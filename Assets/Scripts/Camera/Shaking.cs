using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Shaking : MonoBehaviour
{
    private Vector3 tremblement;
    private Vector3 basePosition;
    [Range(0, 1)]
    [SerializeField]
    private float intensity;
    private float duration;

    private void Start()
    {
        basePosition = transform.position;
        duration = 0f;
    }

    private void Update()
    {
        transform.position = basePosition;
        if (duration > 0f)
            transform.position += Random.insideUnitSphere * intensity;
        duration -= Time.deltaTime;
    }

    public void Shake(float dur)
    {
        duration = dur;
    }
}
