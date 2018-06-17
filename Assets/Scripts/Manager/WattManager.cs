using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WattManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform cursor;
    [SerializeField]
    private Transform ice;
    private List<SwitchLight> allObjs;
    private MusicManager music;
    private MusicManager.Size size;
    private AchievementManager am;
    private PenguinController player;

    private float timer;

    private void Start()
    {
        allObjs = new List<SwitchLight>();
        foreach (GameObject go in FindObjectsOfType<GameObject>())
        {
            SwitchLight sl = go.GetComponent<SwitchLight>();
            if (sl != null)
                allObjs.Add(sl);
        }
        music = GetComponent<MusicManager>();
        size = MusicManager.Size.Low;
        music.Play(MusicManager.Size.Low, 0f);
        //am = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<AchievementManager>();
        timer = 0f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinController>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float watt = (allObjs.Sum(x => x.GetConso()) * 500f) - 250f;
        float cursorWatt = (watt > 240f) ? (240f) : (watt);
        if (cursorWatt < -240f) cursorWatt = -240f;
        cursor.localPosition = new Vector2(0f, cursorWatt);
        float volume = (cursorWatt + 250f) / 500f;
        if (watt < 0)
            watt = 0;
        watt = watt * 0.1f / 250f * Time.deltaTime;
        if (ice.localScale.x - watt > 0.1f)
            ice.localScale = new Vector2(ice.localScale.x - watt, ice.localScale.y - watt);
        else if (timer < 20f && player.IsAlive())
        {
            am.Create("China China China", "You succesfully destroyed your iceberg in less than 10 seconds.");
            timer = 30f;
        }
        else
            Debug.Log(timer);
        if (size == MusicManager.Size.Low && ice.localScale.x < 0.66f)
        {
            size = MusicManager.Size.Medium;
            music.Play(size, volume);
        }
        else if (size == MusicManager.Size.Medium && ice.localScale.x < 0.33f)
        {
            size = MusicManager.Size.High;
            music.Play(size, volume);
        }
        music.SetVolume(MusicManager.Size.Low, volume);
    }
}
