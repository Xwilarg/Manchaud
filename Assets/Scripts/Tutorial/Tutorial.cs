using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    [SerializeField]
    private Text text;
    [SerializeField]
    private string[] texts;
    private float timer;
    private int idx;
    private int step;
    [SerializeField]
    private GameObject orca;
    [SerializeField]
    private Camera penguinCamera, houseCamera;
    [SerializeField]
    private GameObject ice;

	private void Start ()
    {
        idx = 0;
        timer = 0f;
        text.text = texts[idx++];
        step = 12;
        houseCamera.enabled = false;
    }

    private void Update()
    {
        houseCamera.enabled = false;
        timer += Time.deltaTime;
        Debug.Log(timer);
        if ((int)timer == step)
        {
            text.text = texts[idx++];
            timer = 0;
            GetComponent<CanvasRenderer>().SetAlpha(255);
            text.enabled = true;
        }
        if ((int)timer == step - 6)
        {
            GetComponent<CanvasRenderer>().SetAlpha(0);
            text.enabled = false;
            if (idx == 2)
                FindObjectOfType<SpawnOrcaTutorial>().Spawn();
            if (idx == 4)
            {
                penguinCamera.enabled = false;
                houseCamera.enabled = true;
            }
        }
    }

}
