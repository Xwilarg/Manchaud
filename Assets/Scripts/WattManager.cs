using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WattManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform cursor;
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
        if (watt > 250f)
            watt = 250f;
        cursor.localPosition = new Vector2(0f, watt);
    }
}
