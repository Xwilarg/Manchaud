using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Shaking : MonoBehaviour
{
    private Vector3 tremblement;

	private void Update ()
    {
        Vector3 target = new Vector3();
        transform.position = tremblement;

        if (tremblement.magnitude <= target.magnitude + 0.1 && tremblement.magnitude >= target.magnitude - 0.1)
        {
            target = Random.insideUnitCircle;
            target.x -= 21.52f;
        }

        tremblement.x = Mathf.SmoothStep(tremblement.x, target.x, Time.deltaTime * 200);
        tremblement.y = Mathf.SmoothStep(tremblement.y, target.y, Time.deltaTime * 200);
        tremblement.z = Mathf.SmoothStep(tremblement.z, target.z, Time.deltaTime * 200);
    }
}
