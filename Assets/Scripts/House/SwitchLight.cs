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
    private Room room;

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
        room = transform.parent.GetComponent<Room>();
        currSprite = GetComponent<SpriteRenderer>();
        isOn = (currSprite.sprite == spriteOn);
        audioSrc = GetComponent<AudioSource>();
        const float multiplicator = 2f;
        switch (obj)
        {
            case Object.COMPUTER:
                secConso = 0.05f * multiplicator;
                break;

            case Object.FRIDGE:
                secConso = 0.112f * multiplicator;
                break;

            case Object.LIGHHIGH:
            case Object.RADIATOR:
                secConso = 0.076f * multiplicator;
                break;

            case Object.LIGHTLOW:
                secConso = 0.025f * multiplicator;
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
            if (obj != Object.RADIATOR && obj != Object.NOCONSO)
                room.AddLamp();
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
        else if (obj != Object.RADIATOR && obj != Object.NOCONSO)
            room.RemoveLamp();
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
