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

    private void Start()
    {
        allObjs = new List<SwitchLight>();
        foreach (GameObject go in FindObjectsOfType<GameObject>())
        {
            SwitchLight sl = go.GetComponent<SwitchLight>();
            if (sl != null)
                allObjs.Add(sl);
        }
    }

    private void Update()
    {
        float watt = (allObjs.Sum(x => x.GetConso()) * 500f / 0.5f) - 250f;
        float cursorWatt = (watt > 250f) ? (250f) : (watt);
        cursor.localPosition = new Vector2(0f, cursorWatt);
        if (watt < 90)
            watt = 0;
        watt = watt * 0.1f / 250f * Time.deltaTime;
        if (ice.localScale.x - watt > 0.1f)
            ice.localScale = new Vector2(ice.localScale.x - watt, ice.localScale.y - watt);
    }
}
