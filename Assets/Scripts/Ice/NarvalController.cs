using UnityEngine;

public class NarvalController : MonoBehaviour
{
    [SerializeField]
    private float minTime, maxTime;
    [SerializeField]
    private GameObject shadowNarval, hornNarval;

    private Transform playerPos;

    private float timerPrepare;
    private float? timerAim;
    private float? timerRest;

    GameObject curr;

    private void GenerateTimer()
    {
        timerPrepare = Random.Range(minTime, maxTime);
    }

    private void Start()
    {
        GenerateTimer();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        curr = null;
    }

    private void Update()
    {
        if (timerRest != null)
        {
            timerRest -= Time.deltaTime;
            if (timerRest < 0f)
            {
                timerRest = null;
                Destroy(curr);
                GenerateTimer();
            }
        }
        else if (timerAim != null)
        {
            timerAim -= Time.deltaTime;
            if (timerAim < 0f)
            {
                Vector3 pos = curr.transform.position;
                Destroy(curr);
                timerAim = null;
                curr = Instantiate(hornNarval, pos, Quaternion.identity);
                timerRest = 2f;
            }
        }
        else if (timerPrepare < 0f)
        {
            timerAim = 2f;
            curr = Instantiate(shadowNarval, playerPos.position, Quaternion.identity);
        }
        else
            timerPrepare -= Time.deltaTime;
    }
}
