using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private List<AudioSource> ambient;
    private List<AudioSource> vfx;
    [SerializeField]
    private Slider ambientSlider;
    [SerializeField]
    private Slider vfxSlider;

	// Use this for initialization
	void Start () {
        ambient = new List<AudioSource>();
        vfx = new List<AudioSource>();
        foreach (GameObject go in FindObjectsOfType<GameObject>())
        {
            AudioSource source = go.GetComponent<AudioSource>();
            if (source != null)
                if (go.CompareTag("VFX"))
                    vfx.Add(source);
                else
                   ambient.Add(source);
        }
    }
	
	// Update is called once per frame
	void Update () {
        foreach (AudioSource source in ambient)
        {
            source.volume = ambientSlider.value;
        }
        foreach (AudioSource source in vfx)
        {
            source.volume = vfxSlider.value;
        }
	}
}
