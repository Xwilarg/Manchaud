using UnityEngine;

public class NarvalController : MonoBehaviour
{
    [SerializeField]
    private float minTime, maxTime;
    [SerializeField]
    private GameObject shadowNarval, hornNarval;
    [SerializeField]
    private AudioClip attackClip, perceClip;

    private Transform playerPos;

    private float timerPrepare;
    private float? timerAim;
    private float? timerRest;

    private AudioSource source;

    [SerializeField]
    private Camera penguinCamera;

    GameObject curr;

    private void GenerateTimer()
    {
        timerPrepare = Random.Range(minTime, maxTime);
    }

    private void Start()
    {
        GenerateTimer();
        source = GetComponent<AudioSource>();
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
                penguinCamera.GetComponent<Shaking>().Shake(0.5f);
                Vector3 pos = curr.transform.position;
                Destroy(curr);
                source.clip = perceClip;
                source.Play();
                timerAim = null;
                curr = Instantiate(hornNarval, pos, Quaternion.identity);
                timerRest = 2f;
            }
        }
        else if (timerPrepare < 0f)
        {
            timerAim = 1f;
            source.clip = attackClip;
            source.Play();
            curr = Instantiate(shadowNarval, playerPos.position, Quaternion.identity);
        }
        else
            timerPrepare -= Time.deltaTime;
    }
}
