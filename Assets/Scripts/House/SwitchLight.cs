﻿using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class SwitchLight : MonoBehaviour
{
    [SerializeField]
    private Sprite spriteOn, spriteOff;
    private bool isOn;
    private SpriteRenderer currSprite;
    [SerializeField]
    private Type type;
    enum Type { FRIDGE, LIGHT, RADIATOR }
    private AudioSource audioSrc;
    [SerializeField]
    private AudioClip clipOn, clipOff;
    [SerializeField]
    private Object obj;
    private float secConso;
    private float currConso;

    private enum Object
    {
        NOCONSO,
        FRIDGE,
        LIGHHIGH,
        LIGHTLOW,
        COMPUTER,
        RADIATOR
    }

    private void Start()
    {
        currSprite = GetComponent<SpriteRenderer>();
        isOn = (currSprite.sprite == spriteOn);
        audioSrc = GetComponent<AudioSource>();
        switch (obj)
        {
            case Object.COMPUTER:
                secConso = 0.05f;
                break;

            case Object.FRIDGE:
                secConso = 0.112f;
                break;

            case Object.LIGHHIGH:
            case Object.RADIATOR:
                secConso = 0.076f;
                break;

            case Object.LIGHTLOW:
                secConso = 0.025f;
                break;

            default:
                secConso = 0f;
                break;
        }
    }

    public float GetConso()
    {
        if (isOn)
            return (secConso);
        return (0f);
    }

    private void Update()
    {
        currConso += Time.deltaTime * secConso;
    }

    private void OnMouseDown()
    {
        isOn = !isOn;
        currSprite.sprite = (isOn) ? (spriteOn) : (spriteOff);
        audioSrc.PlayOneShot(isOn ? clipOn : clipOff);
    }

    public void SwitchOn()
    {
        if (!isOn)
        {
            currSprite.sprite = spriteOn;
            isOn = true;
            audioSrc.PlayOneShot(clipOn);
        }
    }
}
