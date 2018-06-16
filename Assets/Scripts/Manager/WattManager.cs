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
    }

    private void Update()
    {
        float watt = (allObjs.Sum(x => x.GetConso()) * 500f / 0.5f) - 250f;
        float cursorWatt = (watt > 240f) ? (240f) : (watt);
        if (cursorWatt < -240f) cursorWatt = -240f;
        cursor.localPosition = new Vector2(0f, cursorWatt);
        float volume = (cursorWatt + 250f) / 500f;
        if (watt < 90)
            watt = 0;
        watt = watt * 0.1f / 250f * Time.deltaTime;
        if (ice.localScale.x - watt > 0.1f)
            ice.localScale = new Vector2(ice.localScale.x - watt, ice.localScale.y - watt);
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
