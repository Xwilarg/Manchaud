using UnityEngine;

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
    enum Type { FRIDGE, LIGHT, RADIATOR };
    private AudioSource audioSrc;
    [SerializeField]
    private AudioClip clipOn, clipOff;

    private void Start()
    {
        currSprite = GetComponent<SpriteRenderer>();
        isOn = (currSprite.sprite == spriteOn);
        audioSrc = GetComponent<AudioSource>();
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
