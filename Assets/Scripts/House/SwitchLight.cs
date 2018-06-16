using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class SwitchLight : MonoBehaviour
{
    [SerializeField]
    private Sprite spriteOn, spriteOff;
    [SerializeField]
    private Sprite[] additonalSprites;
    [SerializeField]
    private float[] additonalStats;
    private bool isOn;
    private SpriteRenderer currSprite;
    [SerializeField]
    private Type type;
    enum Type { FRIDGE, LIGHT, RADIATOR, DOOR, COMPUTER }
    private AudioSource audioSrc;
    [SerializeField]
    private AudioClip clipOn, clipOff;
    [SerializeField]
    private Object obj;
    private float secConso;
    private float currConso;

    public Object GetObject() { return (obj); }
    public bool IsOn() { return (isOn); }

    public enum Object
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
        Debug.Assert(additonalStats.Length == additonalSprites.Length, "add sprites and add stats must be same length");
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

    private void SetSprite(string state)
    {
        if (state == "on")
        {
            int i = 0;
            foreach (Sprite s in additonalSprites)
            {
                if (Random.Range(0, 100) < additonalStats[i])
                {
                    currSprite.sprite = s;
                    return;
                }
                i++;
            }
        }
        currSprite.sprite = state == "on" ? spriteOn : spriteOff;
    }

    private void OnMouseDown()
    {
        if (obj != Object.RADIATOR)
        {
            if (isOn)
                SwitchOff();
            else
                SwitchOn();
        }
    }

    public void SwitchOn()
    {
        if (!isOn)
        {
            isOn = true;
            SetSprite("on");
            audioSrc.PlayOneShot(clipOn);
        }
    }

    public void SwitchOff()
    {
        if (isOn)
        {
            isOn = false;
            SetSprite("off");
            audioSrc.PlayOneShot(clipOff);
        }
    }

    public bool IsDoor()
    {
        return type == Type.DOOR;
    }
}
